import { useEffect, useState } from "react";
import { useAuth } from "../AuthContext";
import { useNavigate } from "react-router-dom";
import api from "../api";
import type { Note, CreateNoteRequest } from "../types";
import { AxiosError } from "axios";
import type { ApiError } from "../types";

export default function NotesPage() {
  const { username, logout } = useAuth();
  const navigate = useNavigate();

  const [notes, setNotes] = useState<Note[]>([]);
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [editingId, setEditingId] = useState<number | null>(null);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(true);

  const fetchNotes = async () => {
    try {
      const res = await api.get<Note[]>("/notes");
      setNotes(res.data);
    } catch (err) {
      const axiosError = err as AxiosError<ApiError>;
      if (axiosError.response?.status === 401) {
        logout();
        navigate("/");
        return;
      }
      setError("Failed to load notes.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchNotes();
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    const payload: CreateNoteRequest = { title, content };

    try {
      if (editingId) {
        await api.put(`/notes/${editingId}`, payload);
      } else {
        await api.post("/notes", payload);
      }
      setTitle("");
      setContent("");
      setEditingId(null);
      await fetchNotes();
    } catch (err) {
      const axiosError = err as AxiosError<ApiError>;
      setError(axiosError.response?.data?.message || "Failed to save note.");
    }
  };

  const handleEdit = (note: Note) => {
    setTitle(note.title);
    setContent(note.content);
    setEditingId(note.id);
    setError("");
  };

  const handleDelete = async (id: number) => {
    if (!window.confirm("Are you sure you want to delete this note?")) return;

    try {
      await api.delete(`/notes/${id}`);
      await fetchNotes();
    } catch (err) {
      const axiosError = err as AxiosError<ApiError>;
      setError(axiosError.response?.data?.message || "Failed to delete note.");
    }
  };

  const handleCancel = () => {
    setTitle("");
    setContent("");
    setEditingId(null);
    setError("");
  };

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <div className="notes-container">
      <header className="notes-header">
        <h1>📝 My Notes</h1>
        <div className="header-right">
          <span>Hello, {username}</span>
          <button onClick={handleLogout} className="btn-logout">
            Logout
          </button>
        </div>
      </header>

      {error && <div className="error-message">{error}</div>}

      <form onSubmit={handleSubmit} className="note-form">
        <h2>{editingId ? "Edit Note" : "Create Note"}</h2>
        <div className="form-group">
          <input
            type="text"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Note title"
            required
            maxLength={200}
          />
        </div>
        <div className="form-group">
          <textarea
            value={content}
            onChange={(e) => setContent(e.target.value)}
            placeholder="Note content"
            required
            rows={4}
          />
        </div>
        <div className="form-actions">
          <button type="submit" className="btn-primary">
            {editingId ? "Update" : "Create"}
          </button>
          {editingId && (
            <button type="button" className="btn-secondary" onClick={handleCancel}>
              Cancel
            </button>
          )}
        </div>
      </form>

      <div className="notes-list">
        <h2>Notes ({notes.length})</h2>
        {loading ? (
          <p>Loading notes...</p>
        ) : notes.length === 0 ? (
          <p className="empty-state">No notes yet. Create your first note!</p>
        ) : (
          notes.map((note) => (
            <div key={note.id} className="note-card">
              <div className="note-content">
                <h3>{note.title}</h3>
                <p>{note.content}</p>
                <small>
                  Updated: {new Date(note.updatedAt).toLocaleString()}
                </small>
              </div>
              <div className="note-actions">
                <button onClick={() => handleEdit(note)} className="btn-edit">
                  Edit
                </button>
                <button onClick={() => handleDelete(note.id)} className="btn-delete">
                  Delete
                </button>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  );
}
