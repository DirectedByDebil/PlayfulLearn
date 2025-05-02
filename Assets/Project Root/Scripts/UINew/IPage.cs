using System;

namespace UINew
{
    public interface IPage
    {

        public event Action Closing;


        public void Show();

        public void Hide();
    }
}