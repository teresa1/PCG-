using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class RoomCreatorT : MonoBehaviour {


    public Node parentNode;

    public int roomID;

    public GameObject sibiling { get; private set; }

    //TeresaGrid levegrid;

   
    public void setup()
    {
        //check later
        //transform.position = new Vector3((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);

        transform.position = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z - transform.localScale.z / 2);

        Debug.Log("Tramsform.position.x" + transform.position.x);
        Debug.Log("transform.localScale.x.x" + transform.localScale.x);
        Debug.Log("Tramsform.position.z" + transform.position.z);
        Debug.Log("transform.localScale.z" + transform.localScale.z);
        //
        for (int i = (int)transform.position.x; i < (int)((transform.position.x / 3)+ (transform.localScale.x / 3)); i++)
        {
            for (int j = (int)transform.position.z; j < (int)((transform.position.z /3 )+  (transform.localScale.z /3)); j++)
            {
                TreeStructure.levelGrid[i,j] = 1;
               
            }
        }

        for (int i = 0; i < transform.localScale.x + 1; i++)
        {
            TreeStructure.levelGrid[(int)transform.position.x + i, (int)transform.position.z] = 2;
            TreeStructure.levelGrid[(int)transform.position.x + i, (int)(transform.position.z + transform.localScale.z)] = 2;
        }

        for (int i = 0; i < transform.localScale.z + 1; i++)
        {
            TreeStructure.levelGrid[(int)transform.position.x, (int)transform.position.z + i] = 2;
            TreeStructure.levelGrid[(int)(transform.position.x + transform.localScale.x), (int)transform.position.z + i] = 2;
        }

    }


    public void connect(Node parentNode)
    {
        getSibiling(parentNode);

        if (sibiling != null)
        {
            Vector3 startPos = new Vector3();
            Vector3 endPos = new Vector3();

            if (sibiling.transform.position.z + sibiling.transform.localScale.z / 2 < transform.position.z)
            {
                startPos = chooseDoorPoint(0);
                endPos = sibiling.GetComponent<RoomCreatorT>().chooseDoorPoint(2);
            }
            else if (sibiling.transform.position.z > transform.position.z + transform.localScale.z)
            {
                startPos = chooseDoorPoint(2);
                endPos = sibiling.GetComponent<RoomCreatorT>().chooseDoorPoint(1);
            }
            else if (sibiling.transform.position.x + sibiling.transform.localScale.x < transform.position.x)
            {
                startPos = chooseDoorPoint(3);
                endPos = sibiling.GetComponent<RoomCreatorT>().chooseDoorPoint(1);
            }
            else if (sibiling.transform.position.x > transform.position.x + transform.localScale.x)
            {
                startPos = chooseDoorPoint(1);
                endPos = sibiling.GetComponent<RoomCreatorT>().chooseDoorPoint(3);
            }


            GameObject aDigger = (GameObject)Instantiate(Resources.Load("Digger"), startPos, Quaternion.identity);
            aDigger.GetComponent<Digger>().begin(endPos);


            parentNode = findRoomlessParent(parentNode);

            if (parentNode != null)
            {
                int aC = Random.Range(0, 2);

                if (aC == 0)
                {
                    parentNode.room = gameObject;
                }
                else
                {
                    parentNode.room = sibiling.gameObject;
                }

                sibiling.GetComponent<RoomCreatorT>().parentNode = parentNode;
            }
        }
    }


    public Vector3 chooseDoorPoint(int _index)
    {
        switch (_index)
        {
            case 0:
                return new Vector3((int)(transform.position.x + Random.Range(0, transform.localScale.x - 1)), 
                                        transform.position.y,
                                        (int)transform.position.z);
            case 1:
                return new Vector3((int)(transform.position.x + transform.localScale.x), 
                                    transform.position.y,
                                   (int)(transform.position.z + Random.Range(0, transform.localScale.z - 1)));

            case 2:
                return new Vector3((int)(transform.position.x + Random.Range(1, transform.localScale.x - 2)), 
                                    transform.position.y,
                                   (int)(transform.position.z + transform.localScale.z));
            case 3:
                return new Vector3((int)(transform.position.x + 1), 
                                    transform.position.y,
                                   (int)(transform.position.z + Random.Range(1, transform.localScale.z - 2)));
            default:
                return new Vector3(0, 0, 0);
        }
    }

    public Node findRoomlessParent(Node Node)
    {
        while (true)
        {
            if (Node == null) return null;

            if (Node.room == null) return Node;
            Node = Node.parentNode;
        }
    }

    private void getSibiling(Node parentNode)
    {
        if (parentNode.parentNode != null)
        {
            if (parentNode.parentNode.leftNode != parentNode)
            {
                sibiling = parentNode.parentNode.leftNode.room;
            }
            else
            {
                sibiling = parentNode.parentNode.rightNode.room;
            }
        }
    }


}
