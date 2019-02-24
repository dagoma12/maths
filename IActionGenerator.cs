﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveXFramework
{
    public enum enumConversationState
    {
        ConversationState_Undefined = -1,
        ConversationState_AwaitingReaction = 1,
        ConversationState_AwaitingAction = 2,
        ConversationState_Teach = 4,
        ConversationState_Question = 8,
        ConversationState_Complete = 16
    }

    public interface IConversation
    {
        enumConversationState ConversationState { get; set; }
        IActionGenerator ActionGenerator { get; set; }
        IActionReceptor ActionReceptor { get; set; }
        List<IActionInstance> Actions { get; set; }
        List<IReactionInstance> Reactions { get; set; }
    }

    /// <summary>
    /// A component of an action.
    /// </summary>
    public class SubjectType
    {
        string Name { get; set; }
    }

    public class SubjectInstance
    {
        SubjectType Type { get; set; }
        object Data { get; set; }
    }

    #region Action Details
    public class ActionDetail
    {
        Tuple<object, object> Detail { get; set; } = new Tuple<object, object>(null, null);
    }
    /// <summary>
    /// Type of an action [movement, sound, feel, taste]
    /// </summary>
    public class Action
    {
        string Name { get; set; }
        List<ActionDetail> Details { get; set; } = new List<ActionDetail>();
    }

    /// <summary>
    /// Instance of an action type
    /// </summary>
    public class ActionInstance : IActionInstance
    {
        public Action ActionType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    /// <summary>
    /// interface of an instance of an action type
    /// </summary>
    public interface IActionInstance
    {
        Action ActionType { get; set; }
        object Value { get; set; }      // can be for example a SubjectInstance
    }


    /// <summary>
    /// Generator of actions
    /// </summary>
    public interface IActionGenerator
    {
        /// <summary>
        /// Name can be [eye, ear, nose, tongue, limbs] or  something else capable of an action
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Type of actions allow to be generated by this generator
        /// </summary>
        List<Action> AllowedActions { get; set; }
        IActionInstance GenerateAction(Action actionType, object actionData);
        void CommunicateActionTo(IActionReceptor actionReceptor, IActionInstance actionInstance);
        void ReceiveReaction(IReactionInstance reaction);
    }
    #endregion

    #region Reaction Items
    /// <summary>
    /// Received actions from an IActionGenerator
    /// </summary>
    public interface IActionReceptor
    {
        string Name { get; set; }
        void AddActionInterpretor(IActionInterpretor interpretor);
        /// <summary>
        /// receives an action and passes it on to all its actionInterpretors
        /// </summary>
        /// <param name="action"></param>
        void ReceiveAction(IActionInstance action);
        /// <summary>
        /// responds to an action
        /// </summary>
        /// <returns>reactions, 1 per interpretor</returns>
        List<IReactionInstance> GenerateResponse();
        void CommunicateReactionTo(IActionGenerator actionGenerator, IReactionInstance reactionInstance);
        List<IActionInterpretor> Interpretors { get; set; }
    }

    /// <summary>
    /// Interpretes received actions
    /// </summary>
    public interface IActionInterpretor
    {
        string Name { get; set; }
        List<Action> AllowedActions { get; set; }
        void ReceiveAction(IActionInstance action);
        IReactionInstance GenerateResponse();
    }

    /// <summary>
    /// Type of an reaction [movement, sound, feel, taste]
    /// </summary>
    public class Reaction
    {
        string Name { get; set; }
    }

    /// <summary>
    /// Instance of a reaction type
    /// </summary>
    public class ReactionInstance : IReactionInstance
    {
        public Reaction ActionType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IActionInstance Action { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    /// <summary>
    /// interface of an instance of an reaction type
    /// </summary>
    public interface IReactionInstance
    {
        IActionInstance Action { get; set; }
        Reaction ActionType { get; set; }
        object Value { get; set; }      // can be for example a SubjectInstance
    }
    #endregion
}
