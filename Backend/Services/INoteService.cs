using SimpleNoteApp.DTOs.Notes;

namespace SimpleNoteApp.Services;

public interface INoteService
{
    Task<IEnumerable<NoteResponseDto>> GetAllAsync(int userId);
    Task<NoteResponseDto> CreateAsync(int userId, CreateNoteDto dto);
    Task<NoteResponseDto> UpdateAsync(int userId, int noteId, UpdateNoteDto dto);
    Task DeleteAsync(int userId, int noteId);
}
