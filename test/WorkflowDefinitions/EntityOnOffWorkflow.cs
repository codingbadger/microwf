using tomware.Microwf.Core;
using System;
using System.Collections.Generic;
using tomware.Microwf.Engine;
using System.ComponentModel.DataAnnotations;

namespace microwf.Tests.WorkflowDefinitions
{
  public class EntityOnOffWorkflow : EntityWorkflowDefinitionBase
  {
    public const string TYPE = "EntityOnOffWorkflow";

    public override string Type => TYPE;

    public override Type EntityType => typeof(LightSwitcher);

    public override List<Transition> Transitions
    {
      get
      {
        return new List<Transition>
        {
          new Transition {
            State = "On",
            Trigger = "SwitchOff",
            TargetState ="Off",
            CanMakeTransition = CanSwitch
          },
          new Transition {
            State = "Off",
            Trigger = "SwitchOn",
            TargetState ="On"
          },
        };
      }
    }

    private bool CanSwitch(TransitionContext context)
    {
      if (context.HasVariable<LightSwitcherWorkflowVariable>())
      {
        var variable = context.ReturnVariable<LightSwitcherWorkflowVariable>();

        return variable.CanSwitch;
      }

      return true;
    }
  }

  public class LightSwitcher : IEntityWorkflow
  {
    [Key]
    public int Id { get; set; }
    public string Type { get; set; }
    public string State { get; set; }
    public string Assignee { get; set; }

    public LightSwitcher()
    {
      State = "Off";
      Type = EntityOnOffWorkflow.TYPE;
    }
  }

  public class LightSwitcherWorkflowVariable : WorkflowVariableBase
  {
    public bool CanSwitch { get; set; }
  }
}
