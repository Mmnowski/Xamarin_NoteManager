using System;
using System.Collections.Generic;
using System.Text;
using NoteWizard.Models;

namespace NoteWizard.ViewModels
{
    public class EditViewModel
    {
        public Note Note { get; set; }
        public EditViewModel(Note note = null)
        {
            Note = note;
        }
    }
}
