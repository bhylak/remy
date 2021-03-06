using System;
using System.Threading.Tasks;
using DG.Tweening;

namespace Burners.States
{
    public static class TimerStates
    {        
        public class WaitingForTimerState : BurnerStates.BurnerState
        {
            public WaitingForTimerState(BurnerBehaviour burner, TimeSpan ts) : base(burner)
            {		
                burner._Timer.gameObject.SetActive(true);
                burner._Timer.SetTimer(ts);
                burner._Timer.SetTransparency(0);

                Task.Delay(300).ContinueWith(_ => { _burnerBehaviour._Timer.Show(); });
            }

            public override State Update()
            {
                if (_burnerBehaviour._Timer.isComplete)
                {
                    return new TimerDoneState(_burnerBehaviour);
                }

                return this;
            }
        }
    
        public class TimerDoneState : BurnerStates.BurnerState
        {
            public TimerDoneState(BurnerBehaviour burner) : base(burner)
            {
                _burnerBehaviour.PlayDoneWaitingSound();
                _burnerBehaviour.OnBurnerNotification(
                    new NotificationManager.Notification("Your Timer is Done",
                        _burnerBehaviour));
            }

            public override State Update()
            {
                if (RecipeManager.Instance.IsRecipeInProgress)
                {
                    return null;
                }
                else if (!_burnerBehaviour._model.IsPotDetected.Value)
                {
                    _burnerBehaviour._Timer.Reset();
                    return new BurnerStates.AvailableState(_burnerBehaviour);
                }
                else
                {
                    return this;
                }
                
                //if is dismissed, transition out, return waiting state.
            }
        }   
    }
}