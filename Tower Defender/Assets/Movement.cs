using UnityEngine;

public class Movement : MonoBehaviour
{
    private string nome;

    [SerializeField] private float velocidade = 1f;

    Rigidbody rb = null;

    private void Awake()
    {

        rb = GetComponent<Rigidbody>();

    }


    // Update is called once per frame
    void Update()
    {
        
        Vector3 posicaoAtual = new Vector3(transform.position.x, transform.position.y, transform.position.z);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            posicaoAtual += new Vector3(posicaoAtual.x, posicaoAtual.y + velocidade * Time.deltaTime, posicaoAtual.z);
        }


        rb.MovePosition(posicaoAtual);

    }


    protected void MovimentaPersonagem()
    {



    }

    public int CalculaArea(float basee, float altura)
    {

        return ((int) basee * (int) altura);

    }

}

public class Seila : Movement
{

    private void Update()
    {

        Seila seila = new Seila();
        seila.MovimentaPersonagem();

    }

}
