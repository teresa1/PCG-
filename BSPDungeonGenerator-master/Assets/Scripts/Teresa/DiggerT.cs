using UnityEngine;

public class DiggerT : MonoBehaviour
{


    private Vector3 targetPos;

    void Start()
    {
        // targetPos = _targetPos;

        dig();
    }

    public void dig()
    {
        while (transform.position.x != targetPos.x)
        {
            if (transform.position.x < targetPos.x)
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            }

            updateTile();
        }

        while (transform.position.z != targetPos.z)
        {
            if (transform.position.z < targetPos.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            }

            updateTile();
        }

        DestroyImmediate(this);
    }


    public void surroundTilesWithWall(int _x, int _y)
    {
        if (TreeStructure.levelGrid[_x + 1, _y] == 0)
        {
            TreeStructure.levelGrid[_x + 1, _y] = 2;
        }

        if (TreeStructure.levelGrid[_x - 1, _y] == 0)
        {
            TreeStructure.levelGrid[_x + 1, _y] = 2;
        }

        if (TreeStructure.levelGrid[_x - 1, _y + 1] == 0)
        {
            TreeStructure.levelGrid[_x, _y + 1] = 2;
        }

        if (TreeStructure.levelGrid[_x - 1, _y - 1] == 0)
        {
            TreeStructure.levelGrid[_x, _y - 1] = 2;
        }
    }



    private void updateTile()
    {

        TreeStructure.levelGrid[(int)transform.position.x, (int)transform.position.z] = 1;
        TreeStructure.levelGrid[(int)transform.position.x + 1, (int)transform.position.z] = 1;
        TreeStructure.levelGrid[(int)transform.position.x - 1, (int)transform.position.z] = 1;
        TreeStructure.levelGrid[(int)transform.position.x, (int)transform.position.z + 1] = 1;
        TreeStructure.levelGrid[(int)transform.position.x, (int)transform.position.z - 1] = 1;

        surroundTilesWithWall((int)transform.position.x + 1, (int)transform.position.z);
        surroundTilesWithWall((int)transform.position.x - 1, (int)transform.position.z);
        surroundTilesWithWall((int)transform.position.x, (int)transform.position.z + 1);
        surroundTilesWithWall((int)transform.position.x, (int)transform.position.z - 1);
    }

}