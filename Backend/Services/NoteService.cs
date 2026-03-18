using Microsoft.EntityFrameworkCore;
using SimpleNoteApp.Data;
using SimpleNoteApp.DTOs.Notes;
using SimpleNoteApp.Models;

namespace SimpleNoteApp.Services;

public class NoteService : INoteService
{
    private readonly AppDbContext _context;

    public NoteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NoteResponseDto>> GetAllAsync(int userId)
    {
        return await _context.Notes
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.UpdatedAt)
            .Select(n => new NoteResponseDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedAt = n.CreatedAt,
                UpdatedAt = n.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<NoteResponseDto> CreateAsync(int userId, CreateNoteDto dto)
    {
        var note = new Note
        {
            Title = dto.Title,
            Content = dto.Content,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        return new NoteResponseDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt
        };
    }

    public async Task<NoteResponseDto> UpdateAsync(int userId, int noteId, UpdateNoteDto dto)
    {
        var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId);

        if (note == null)
            throw new KeyNotFoundException("Note not found.");

        note.Title = dto.Title;
        note.Content = dto.Content;
        note.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new NoteResponseDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt
        };
    }

    public async Task DeleteAsync(int userId, int noteId)
    {
        var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId);

        if (note == null)
            throw new KeyNotFoundException("Note not found.");

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
    }
}
