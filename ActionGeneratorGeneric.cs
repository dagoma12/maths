using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveXFramework
{
    public class ActionGeneratorGeneric : IActionGenerator
    {
        private List<Action> _allowedAction = new List<Action>();
        public ActionGeneratorGeneric()
        {
            AllowedActions = _allowedAction;
        }
        public string Name { get; set; }
        public List<Action> AllowedActions {get; set; }

        public IActionInstance GenerateAction(Action actionType, object actionData)
        {
            throw new NotImplementedException();
        }
    }
}
