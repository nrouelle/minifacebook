using MiniFacebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFacebook.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
    }
}
