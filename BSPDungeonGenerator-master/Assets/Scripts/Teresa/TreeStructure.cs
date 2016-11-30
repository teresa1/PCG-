using UnityEngine;
using System.Collections;
using System.Security;

public class TreeStructure : MonoBehaviour
{
    public static TeresaGrid levelGrid;

    public Node parentNode;

    private int roomID;

    RoomCreatorT roomCreatorT;

    private int bridgeSize = 5;

    private void Start()
    {
       
        //cria o cubo e dá um tamanho
        GameObject startCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        startCube.transform.localScale = new Vector3(150, 1, 150);
        startCube.tag = "GenSection";

        //posiciona 
        startCube.transform.position = new Vector3(transform.position.x + startCube.transform.localScale.x / 2,
                                                   transform.position.y,
                                                   transform.position.z + startCube.transform.localScale.z / 2);

        //cria uma grid do tamanho do cubo inicializada a 0 
        levelGrid = new TeresaGrid(300,300);
        levelGrid.Inicialize();
      

        // indica o parent node
        parentNode = new Node();
        parentNode.cube = startCube;

        //nº de salas que vai fazer
        for (int i = 0; i < 2; i++)
        {
            split(parentNode);
        }

        //create the rooms
        createRooms(parentNode);

        //connect the rooms


        this.gameObject.AddComponent<RoomCreatorT>().connect(parentNode);

        //roomCreatorT.connect(parentNode);

        //tidy up dungeon      Removes singles
        for (int k = 0; k < 5; k++)
        {
            for (int i = 0; i < levelGrid.gridWidth; i++)
            {
                for (int j = 0; j < levelGrid.gridHeight; j++)
                {
                    removeSingles(i, j);
                }
            }
        }
        
        createLevel();
    }

    //divide a tree
    public void split(Node Node)
    {
        if (Node.leftNode != null)
        {
            split(Node.leftNode);
        }
        else
        {
            //divide o nodo
            Node.cut();
            return;
        }

        if (Node.rightNode != null)
        {
            split(Node.rightNode);
        }
    }

    private void addRoom(Node Node)
    {
        GameObject aObj = Node.cube;

        GameObject Room = (GameObject)Instantiate(Resources.Load("BaseRoom"), aObj.transform.position, Quaternion.identity);

        //cria os quartos com o tamanho da sala -5 - not random anymore
        Room.transform.localScale = new Vector3(
            (aObj.transform.localScale.x - bridgeSize),
            Room.transform.localScale.y,
            (aObj.transform.localScale.z - bridgeSize));

        
        Room.GetComponent<RoomCreatorT>().setup();
        Room.GetComponent<RoomCreatorT>().roomID = roomID;
        Room.GetComponent<RoomCreatorT>().parentNode = Node;
        Node.room = Room;
        roomID++;
    }

    private void createRooms(Node Node)
    {
        if (Node.leftNode != null)
        {
            createRooms(Node.leftNode);
        }
        else
        {
            addRoom(Node);
            return;
        }

        if (Node.rightNode != null)
        {
            createRooms(Node.rightNode);
        }
    }


       private void createLevel()
    {
        for (int i = 0; i < levelGrid.gridWidth; i++)
        {
            for (int j = 0; j < levelGrid.gridHeight; j++)
            {
                switch (levelGrid[i, j])
                {
                    case 0:
                        Instantiate(Resources.Load("River"), new Vector3(transform.position.x - transform.localScale.x / 2
                                                                             + i, transform.position.y + transform.localScale.y / 2,
                                                                             transform.position.z - transform.localScale.z / 2 + j), Quaternion.identity);
                        break;

                    case 1:
                        Instantiate(Resources.Load("FloorTile"), new Vector3(transform.position.x - transform.localScale.x / 2
                                                                             + i, transform.position.y + transform.localScale.y / 2,
                                                                             transform.position.z - transform.localScale.z / 2 + j), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Resources.Load("WallTile"), new Vector3(transform.position.x - transform.localScale.x / 2 +
                                                                            i, transform.position.y + transform.localScale.y / 2,
                                                                            transform.position.z - transform.localScale.z / 2 + j), Quaternion.identity);
                        break;
                }
            }
        }
    }


    //cellular automota rules for cleanup stage
    private void removeSingles(int _x, int _y)
    {
        int count = 0;

        if (_x < levelGrid.gridWidth - 1 && _x > 1 && _y > 1 && _y < levelGrid.gridHeight - 1)
        {
            if (levelGrid[_x + 1, _y] == 1)
            {
                count++;
            }

            if (levelGrid[_x - 1, _y] == 0)
            {
                return;
            }

            if (levelGrid[ _x + 1, _y] == 0)
            {
                return;
            }

            if (levelGrid[_x, _y + 1] == 0)
            {
                return;
            }

            if (levelGrid[_x, _y - 1] == 0)
            {
                return;
            }


            //

            if (levelGrid[_x - 1, _y] == 1)
            {
                count++;
            }

            if (levelGrid[_x, _y + 1] == 1)
            {
                count++;
            }

            if (levelGrid[_x, _y - 1] == 1)
            {
                count++;
            }

            if (levelGrid[_x - 1, _y] == 1)
            {
                count++;
            }

            if (levelGrid[_x - 1, _y - 1] == 1)
            {
                count++;
            }

            if (levelGrid[_x + 1, _y - 1] == 1)
            {
                count++;
            }

            if (levelGrid[_x - 1, _y + 1] == 1)
            {
                count++;
            }

            if (levelGrid[_x + 1, _y + 1] == 1)
            {
                count++;
            }

            if (count >= 5)
            {
                levelGrid[_x, _y] = 1;
            }
        }
    }
}
