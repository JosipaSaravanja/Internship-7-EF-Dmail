using Dmail.Presentation.Actions.Main;

namespace Dmail.Presentation.Actions
{
    public class LogOutAction : ExitAction
    {
        private readonly Action _logoutFunc;

        public int Index { get; set; }
        public override string Name { get; set; } = "Odjava";

        public LogOutAction(Action logoutFunc)
        {
            _logoutFunc = logoutFunc;
        }

        public override void Open()
        {
            _logoutFunc();
        }
    }
}