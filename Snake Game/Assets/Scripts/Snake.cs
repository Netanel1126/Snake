using System.Collections;
using System.Collections.Generic;
using CodeMonkey;
using CodeMonkey.Utils;
using UnityEngine;

enum MoveDirectionEnum
{
    UP = 0,
    DOWN,
    LEFT,
    RIGHT
}

enum State
{
    Alive,
    Dead
}

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private Vector2Int gridPosition;
    private MoveDirectionEnum moveDirctionEnum;
    private LevelGrid levelGrid;
    private int snakeBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartsList;
    private State state;

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        gridMoveTimerMax = 0.3f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);
        moveDirctionEnum = MoveDirectionEnum.RIGHT;

        snakeBodySize = 0;
        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodyPartsList = new List<SnakeBodyPart>();
        state = State.Alive;
    }

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    private void Update()
    {
        if(state == State.Alive)
        {
            HandleInput();
            HandleGridMovment();
        }
    }

    private void HandleInput()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && moveDirctionEnum != MoveDirectionEnum.DOWN)
        {
            moveDirctionEnum = MoveDirectionEnum.UP;
            gridMoveDirection = new Vector2Int(0, 1);
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && moveDirctionEnum != MoveDirectionEnum.UP)
        {
            moveDirctionEnum = MoveDirectionEnum.DOWN;
            gridMoveDirection = new Vector2Int(0, -1);
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && moveDirctionEnum != MoveDirectionEnum.RIGHT)
        {
            moveDirctionEnum = MoveDirectionEnum.LEFT;
            gridMoveDirection = new Vector2Int(-1, 0);
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && moveDirctionEnum != MoveDirectionEnum.LEFT)
        {
            moveDirctionEnum = MoveDirectionEnum.RIGHT;
            gridMoveDirection = new Vector2Int(1, 0);
        }

    }

    private void HandleGridMovment()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;
            SoundManager.PlaySound(Sounds.SnakeMove);

            if (levelGrid.TrySbakeEatFood(gridPosition))
            {
                snakeBodySize++;
                CreateSnakeBody();
                SoundManager.PlaySound(Sounds.SnakeEat);
            }

            SnakeMovePosition preivios = null;
            if (snakeMovePositionList.Count > 0)
            {
                preivios = snakeMovePositionList[0];
            }

            snakeMovePositionList.Insert(0, new SnakeMovePosition(preivios,gridPosition,moveDirctionEnum));

            gridPosition += gridMoveDirection;
            gridPosition = levelGrid.ValidateGridPosition(gridPosition);

            if (snakeMovePositionList.Count >= snakeBodySize + 1){
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            foreach(SnakeBodyPart snakeBodyPart in snakeBodyPartsList)
            {
                if(gridPosition == snakeBodyPart.GridPosition)
                {
                    state = State.Dead;
                    GameHandler.SnakeDied();
                    SoundManager.PlaySound(Sounds.SnakeDie);
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);
            UpdateSnakeBodyPart();
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }

    private void UpdateSnakeBodyPart()
    {
        for (int i = 0; i < snakeBodyPartsList.Count; i++)
        {
            snakeBodyPartsList[i].SetSnakeMovePostion(snakeMovePositionList[i]);
        }
    }

    public Vector2Int GetGridPosition()
    {
        return this.gridPosition;
    }

    public List<Vector2Int> GetFullSnakeGridPosaitionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            gridPositionList.Add(snakeMovePosition.GridPosition);
        }
        return gridPositionList;
    }

    private void CreateSnakeBody()
    {
        snakeBodyPartsList.Add(new SnakeBodyPart(snakeBodyPartsList.Count));
    }

    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePostion(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GridPosition.x, snakeMovePosition.GridPosition.y);

            float angle = 0;
            switch (snakeMovePosition.Direction)
            {
                case MoveDirectionEnum.UP:
                     switch(snakeMovePosition.PreviousSnakeMovePositionDirection)
                    {
                        case MoveDirectionEnum.LEFT:
                            angle = 45;
                            break;
                        case MoveDirectionEnum.RIGHT:
                            angle = -45;
                            break;
                        default:
                            angle = 0;
                            break;
                    }
                    break;
                case MoveDirectionEnum.DOWN:
                    switch (snakeMovePosition.PreviousSnakeMovePositionDirection)
                    {
                        case MoveDirectionEnum.LEFT: angle = 180 + 45; break;
                        case MoveDirectionEnum.UP: angle = 180 - 45; break;
                        default: angle = 180; break;
                    };
                    break;
                case MoveDirectionEnum.LEFT:
                    switch (snakeMovePosition.PreviousSnakeMovePositionDirection)
                    {
                        case MoveDirectionEnum.DOWN: angle = -45; break;
                        case MoveDirectionEnum.UP: angle = 45; break;
                        default: angle = -90; break;
                    };
                    break;
                case MoveDirectionEnum.RIGHT:
                    switch (snakeMovePosition.PreviousSnakeMovePositionDirection)
                    {
                        case MoveDirectionEnum.DOWN: angle = 45; break;
                        case MoveDirectionEnum.UP: angle = -45; break;
                        default: angle = 90; break;
                    };
                    break;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int? GridPosition { get => snakeMovePosition?.GridPosition; }

    }


    private class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private MoveDirectionEnum direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition,Vector2Int gridPosition, MoveDirectionEnum direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.direction = direction;
            this.gridPosition = gridPosition;
        }

        public Vector2Int GridPosition { get => gridPosition; }
        public MoveDirectionEnum Direction { get => direction; }
        public MoveDirectionEnum PreviousSnakeMovePositionDirection { get => previousSnakeMovePosition == null ? MoveDirectionEnum.RIGHT : previousSnakeMovePosition.Direction; }

    }
}
