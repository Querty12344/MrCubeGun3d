using System;

namespace Infrastructure.Adds
{
    public interface IAddsService
    {
        public void ShowFullScreenAdd(Action onClosed = null);
        public void ShowVideoAdd(Action onVideoEnded = null);
    }
}