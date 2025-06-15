using Playables;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace LessonsPractices.Blocks
{
    public sealed class AnimationBasicsBlocks : MonoBehaviour, IBlocksPractice
    {

        public event Action<bool> IsCompletedChanged;


        [SerializeField, Space]
        private Player _player;

        [SerializeField, Space]
        private PlayerView _playerView;


        [SerializeField, Space]
        private Transform _pointA;

        [SerializeField, Space]
        private Transform _pointB;

        [SerializeField, Space, Range(0, 2f)]
        private float _interpolationTime;


        private Transform _lastPoint;


        private IList<BlockButton> _blockButtons;

        private IList<Action> _buttonClicks;


        private BlockButton _selectedCondition;

        private BlockButton _selectedAction;

        private BlockButton _selectedAnimation;


        private bool _isPlaying;



        private void Update()
        {
            
            if(_isPlaying && IsCondition())
            {

                _player.StopMovement();

                _playerView.MakeIdle();


                ViewAction();

                CallAnimation();

                _isPlaying = false;
            }
        }


        public void Init()
        {

            _blockButtons = new List<BlockButton>();

            _buttonClicks = new List<Action>();


            _player.Init();

            _playerView.Init();


            _player.HasGrounded += _playerView.StopJump;

            _player.StoppingRunning += _playerView.MakeIdle;


            _lastPoint = _pointA;
        }
        

        public void SetInputs(IEnumerable<InputField> inputs)
        {

            _buttonClicks.Clear();

            _blockButtons.Clear();


            foreach(InputField field in inputs)
            {

                BlockButton blockButton = new (field.Block, field.Button);


                Action click = () =>
                {
                    SelectBlock(blockButton);
                };


                blockButton.Clicked += click;


                _buttonClicks.Add(click);

                _blockButtons.Add(blockButton);
            }
        }


        public void StartGame()
        {

            if(_selectedCondition != null &&
                _selectedAction != null &&
                _selectedAnimation != null)
            {

                _isPlaying = true;
            }
        }


        public bool IsCompleted()
        {

            if(_selectedCondition == null ||
                _selectedAction == null ||
                _selectedAnimation == null)
            {

                return false;
            }


            return _selectedCondition.IsAnswer &&
                _selectedAction.IsAnswer &&
                _selectedAnimation.IsAnswer;
        }


        public void Unload()
        {

            _player.HasGrounded -= _playerView.StopJump;

            _player.StoppingRunning -= _playerView.MakeIdle;


            for (int i = 0; i < _blockButtons.Count; i++)
            {

                _blockButtons[i].Clicked -= _buttonClicks[i];
            }

            _buttonClicks.Clear();

            _blockButtons.Clear();
        }


        #region Select/Change Block

        private void SelectBlock(BlockButton block)
        {

            switch (block.BlockType)
            {

                case BlockType.Condition:

                    ChangeBlock(ref _selectedCondition, block);            
                    break;

                
                case BlockType.Action:

                    ChangeBlock(ref _selectedAction, block);            
                    break;
                

                case BlockType.Animation:

                    ChangeBlock(ref _selectedAnimation, block);            
                    break;
            }
        }


        private void ChangeBlock(ref BlockButton selected, BlockButton newBlock)
        {

            if (selected == newBlock) return;


            selected?.SetSelected(false);

            newBlock.SetSelected(true);


            selected = newBlock;
        }

        #endregion


        #region Blocks Actions

        private bool IsCondition()
        {

            if (_selectedCondition == null) return false;


            return _selectedCondition.ConditionType switch
            {

                ConditionType.Always => true,

                ConditionType.PressJump => Input.GetButtonDown("Jump"),

                ConditionType.GetAxis => Input.GetAxis("Horizontal") != 0,

                ConditionType.Fire => Input.GetButtonDown("Fire1"),

                _ => false,
            };
        }


        private void ViewAction()
        {

            switch (_selectedAction.ActionType)
            {
                case ActionType.Jump:

                    _player.Jump();
                    break;
                
                case ActionType.Run:

                    _player.MoveToPosition(GetPoint(), _interpolationTime);
                    break;
                
                case ActionType.Fire:
                    break;
            }

        }


        private void CallAnimation()
        {

            switch (_selectedAnimation.AnimationType)
            {

                case AnimationType.Jump:

                    _playerView.StartJump();
                    break;

                case AnimationType.Run:

                    _playerView.ViewMovement(GetViewDirection());
                    break;

                case AnimationType.Fire:
                    break;
            }

        }


        private Vector3 GetPoint()
        {

            if(_lastPoint == _pointA)
            {
                _lastPoint = _pointB;
            }
            else
            {
                _lastPoint = _pointA;
            }


            return _lastPoint.position;
        }    


        private int GetViewDirection()
        {

            Transform min = _pointA.position.x < _pointB.position.x ? _pointA: _pointB;

            if (_lastPoint == min)
                return -1;

            return 1;
        }
        
        #endregion
    }
}
