using UnityEngine;
using System.Collections;
using System.Xml;

public class Node
{
    public GameObject cube;
    public Node parentNode { get; private set; }
    public Node setParentNode { get; private set; }
    public Node leftNode { get; private set; }
    public Node rightNode { get; private set; }
    public Color Color { get; private set; }
  

    private bool isConnected = false;

    public GameObject room;

    public Node()
    {
        Color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    void splitX(GameObject Section)
    {
        float xSplit = Random.Range(20, Section.transform.localScale.x - 20);

        //tamanho min
        if (xSplit > 50)
        {
            //cria o cubo
            GameObject cube0 = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //poe o cubo do tamanho da secção já com o X dividido
            cube0.transform.localScale = new Vector3(xSplit, Section.transform.localScale.y, Section.transform.localScale.z);

            //posiciona o cubo tendo em conta o que foi cortado
            cube0.transform.position = new Vector3(
                Section.transform.position.x - ((xSplit - Section.transform.localScale.x) / 2),
                Section.transform.position.y,
                Section.transform.position.z);
         
            cube0.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            cube0.tag = "GenSection";


            leftNode = new Node();
            leftNode.cube = cube0;
            leftNode.setParentNode = this;


            //cria outro cubo
            GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //escala e posiciona tendo em conta o xSplit
            float split1 = Section.transform.localScale.x - xSplit;
            cube1.transform.localScale = new Vector3(split1, Section.transform.localScale.y, Section.transform.localScale.z);
            cube1.transform.position = new Vector3(
                Section.transform.position.x + ((split1 - Section.transform.localScale.x) / 2),
                Section.transform.position.y,
                Section.transform.position.z);


            cube1.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            cube1.tag = "GenSection";
            rightNode = new Node();
            rightNode.cube = cube1;
            rightNode.setParentNode = this;

            GameObject.DestroyImmediate(Section);
        }
    }

    void splitZ(GameObject Section)
    {
        float zSplit = Random.Range(15, Section.transform.localScale.z - 20);
        float zSplit1 = Section.transform.localScale.z - zSplit;

        //tamanho min
        if (zSplit > 50)
        {
            //cria o cubo
            GameObject cube0 = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //poe o cubo do tamanho da secção já com o Z dividido
            cube0.transform.localScale = new Vector3(Section.transform.localScale.x, Section.transform.localScale.y, zSplit);

            //posiciona o cubo tendo em conta o que foi cortado
            cube0.transform.position = new Vector3(
                Section.transform.position.x,
                Section.transform.position.y,
                Section.transform.position.z - ((zSplit - Section.transform.localScale.z) / 2));

            cube0.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            cube0.tag = "GenSection";

            leftNode = new Node();
            leftNode.cube = cube0;
            leftNode.setParentNode = this;

            //cria o outro cubo
            GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);


            //escala e posiciona tendo em conta o zSplit
            cube1.transform.localScale = new Vector3(Section.transform.localScale.x, Section.transform.localScale.y, zSplit1);
            cube1.transform.position = new Vector3(
                Section.transform.position.x,
                Section.transform.position.y,
                Section.transform.position.z + ((zSplit1 - Section.transform.localScale.z) / 2));

            cube1.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            cube1.tag = "GenSection";
            rightNode = new Node();
            rightNode.cube = cube1;
            rightNode.setParentNode = this;

            GameObject.DestroyImmediate(Section);
        }
    }

    public void cut()
    {
        float choice = Random.Range(0, 2);
        if (choice <= 0.5)
        {
            splitX(cube);
        }
        else
        {
            splitZ(cube);
        }
    }


}
