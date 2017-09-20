using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LaserScript : MonoBehaviour {

    public float mFireRate = .5f;
    public float mFireRange = 50f;
    public float mHitForce = 100f;
    public int mLaserDamage = 100;

    private LineRenderer mLaserLine;

    private bool mLaserLineEnabled;

    private WaitForSeconds mLaserDuration = new WaitForSeconds(0.05f);

    private float mNextFire;

    private AudioSource laser;

    int contador = 0;
	string PalZSC= "ZSC";
	string PalZSI="ZSI";
	string PalCZC= "CZC";
	string PalCZI="CZI";
	string PalSCC= "SCC";
	string PalSCI="SCI";
	string keyC;
	string keyI;
	int RangoZS=23;
	int RangoCZ=46;
	int RangoSC=50;
    public int TipoJuego=2;//=0;
	//Juego ZS=0
	//Juego ZC=1
	//Juego SC=2
	int NPalabra;
    int LimiteRango;

	string val;
	Dictionary<string, string> DPalabras = new Dictionary<string, string>();
    public Text puntuacion;
	public TextMesh TextoPalabraC;
	public TextMesh TextoPalabraI;
    public Text puntuacionFinal;

    public RawImage Vida1;
    public RawImage Vida2;
    public RawImage Vida3;
    int vida = 3;


    void Start()
    {
        if(TipoJuego == 0)
        {
            LimiteRango = RangoZS;
        }
        if(TipoJuego == 1)
        {
            LimiteRango = RangoCZ;
        }
        if(TipoJuego == 2)
        {
            LimiteRango = RangoSC;
        }
		TextAsset asset = Resources.Load("Palabras") as TextAsset;
		if (asset !=null){
			Debug.Log ("IS NOT NULL");
			Load (asset);
		}
        /*EstadoJuego variable = GetComponent<EstadoJuego>();
        TipoJuego = variable.variable;
        Debug.Log("TipoJuego es " + TipoJuego);*/
        NPalabra = Random.Range (1,LimiteRango);
		AsignarPalabras (NPalabra);
		//ByteReader reader = new ByteReader(asset);
		//DPalabras = reader.ReadDictionary ();
		//char[] splitby = System.Environment.NewLine.ToCharArray();
		//if (asset != null) {
		//	DPalabras = asset.text.Split (splitby).ToDictionary (s=>s,s=>true);
		//}

		//Debug.Log ("Palabra= " + DPalabras.TryGetValue (key, out val));
        mLaserLine = GetComponent<LineRenderer>();
        laser = GetComponent<AudioSource>();
        puntuacionFinal.enabled = false;
        

    }

	void Load (TextAsset asset)
	{
		
		ByteReader reader = new ByteReader(asset);
		DPalabras = reader.ReadDictionary();

	}

	public string Get (string key)
	{
		return (DPalabras.TryGetValue(key, out val)) ? val : key;

	}

    private void Fire()
    {
        Transform cam = Camera.main.transform;

        mNextFire = Time.time + mFireRate;

        Vector3 rayOrigin = cam.position;

        mLaserLine.SetPosition(0, transform.up * -10f);

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, cam.forward, out hit, mFireRange))
        {
			NPalabra = Random.Range (1,LimiteRango);

            mLaserLine.SetPosition(1, hit.point);

            CubeBehaviorScript cubeCtr = hit.collider.GetComponent<CubeBehaviorScript>();
            CubeBehaviorScript2 cubeCtr2 = hit.collider.GetComponent<CubeBehaviorScript2>();
            if (cubeCtr != null)
            {
                if (hit.rigidbody != null)
                {
                    contador = contador + 5;
                    Debug.Log("Tiro");
                    Debug.Log("Contador " + contador);
                    puntuacion.text = "Marcador: " + contador;
                    hit.rigidbody.AddForce(-hit.normal * mHitForce);
                    cubeCtr.Hit(mLaserDamage);
					AsignarPalabras (NPalabra);
                }
            }

            if (cubeCtr2 !=null)
            {
                if(hit.rigidbody != null)
                {
                    vida = vida - 1;
                    Debug.Log("Incorrecto");
                    hit.rigidbody.AddForce(-hit.normal * mHitForce);
                    cubeCtr2.Hit(mLaserDamage);
					AsignarPalabras (NPalabra);
                }
            }

            if(vida <= 0)
            {
                Vida1.enabled = false;
                puntuacionFinal.enabled = true;
                puntuacionFinal.text = "Puntuacion Total: " + contador;
                //Application.Quit();
            }
            if (vida == 3)
            {
                Vida1.enabled = true;
                Vida2.enabled = true;
                Vida3.enabled = true;
            }
            if(vida == 2)
            {
                Vida1.enabled = true;
                Vida2.enabled = true;
                Vida3.enabled = false;
            }
            if(vida == 1)
            {
                Vida1.enabled = true;
                Vida2.enabled = false;
                Vida3.enabled = false;
            }

        }
        else
        {
            mLaserLine.SetPosition(1, cam.forward * mFireRange);
        }

        StartCoroutine(LaserFx());
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > mNextFire)
        {
            Fire();
            laser.Play();
        }
    }

	void AsignarPalabras(int NPalabra)
	{
		Debug.Log ("Num: " + NPalabra);
		if (TipoJuego == 0) {
			keyC = PalZSC + NPalabra;
			keyI = PalZSI + NPalabra;
		}
		if (TipoJuego == 1) {
			keyC = PalCZC + NPalabra;
			keyI = PalCZI + NPalabra;
		}
		if (TipoJuego == 2) {
			keyC = PalSCC + NPalabra;
			keyI = PalSCI + NPalabra;
		}
		Debug.Log ("KEY= "+keyC);
		//Debug.Log("Pal: "+Get("ZC1"));
		Debug.Log("PalC: "+Get(keyC));
		Debug.Log("PalI: "+Get(keyI));
		//TextMesh textObject = GameObject.Find ("Sphere").GetComponent<TextMesh>();
		TextoPalabraC.text = Get (keyC);
		TextoPalabraI.text = Get (keyI);
	}

    private IEnumerator LaserFx()
    {
        mLaserLine.enabled = true;


        yield return mLaserDuration;
        mLaserLine.enabled = false;
    }
}
