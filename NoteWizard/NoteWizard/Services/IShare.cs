using NoteWizard.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteWizard.Services
{
    public interface IShare
    {
        Task ShareAsync(Note item);
    }
}
