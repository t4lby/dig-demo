using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour
{
    public GameObject DirtPrefab;
    public Sprite DirtCornerSprite;
    public Sprite[] DirtSprites;
    public int Width;
    public int Height;
    public float PixelHeight;
    public float PixelWidth;
    public GameObject[] Characters;

    private bool[,] _bitMap;
    private GameObject[,] _dirtMap;

    const float NE_ROTATION = 0f;
    const float NW_ROTATION = 90f;
    const float SW_ROTATION = 180f;
    const float SE_ROTATION = 270f;

    private void Start()
    {
        _bitMap = MapGenerators.Circle(Width, Height, 15, new Vector2(Width / 4, Height / 2));
        _bitMap = MapGenerators.Circle(Width, Height, 15, new Vector2(Width * 3 / 4, Height / 2), _bitMap);
        _dirtMap = new GameObject[Width, Height];

        //Set the bitmap to true, would load some pre config or generate in practice.
        /*
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _bitMap[i,j] = true;
            }
        }*/

        // Draw the squares based on the bitmap.
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _dirtMap[i, j] = Instantiate(DirtPrefab, this.transform);
                _dirtMap[i, j].transform.localPosition =
                    new Vector2(i * PixelWidth, -j * PixelHeight);
                _dirtMap[i, j].transform.localScale =
                    new Vector3(PixelWidth, PixelHeight, 0);
                var dirt = _dirtMap[i, j].GetComponent<Dirt>();
                dirt.Map = this;
                dirt.MapIndexA = i;
                dirt.MapIndexB = j;
                _dirtMap[i, j].GetComponent<SpriteRenderer>().sprite =
                    DirtSprites[Random.Range(0, DirtSprites.Length)];

            }
        }

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (!_bitMap[i, j])
                {
                    RemoveDirt(i, j);
                }
            }
        }
    }

    public void RemoveDirt(int a, int b)
    {
        // Remove the colliders of the dirt and adjust surrounding dirt sprites.
        _dirtMap[a,b].GetComponent<Collider2D>().enabled = false;
        _dirtMap[a,b].GetComponent<SpriteRenderer>().enabled = false;
        _bitMap[a, b] = false;

        //Asses whether surrounding squares need replacing with a corner.
        /*
        if (b > 0 && !_bitMap[a, b - 1])
        {
            if (a > 0 && !_bitMap[a - 1, b - 1])
            {
                //replace left square with triangle sw
                ChangeToTriange(_dirtMap[a - 1, b], SW_ROTATION);
            }
            if (a + 1 < Width && !_bitMap[a + 1, b - 1])
            {
                // replace right square with triangle se
                ChangeToTriange(_dirtMap[a + 1, b], SE_ROTATION);
            }
        }
        if (b + 1 < Height && !_bitMap[a, b + 1])
        {
            if (a > 0 && !_bitMap[a - 1, b + 1])
            {
                //replace left square with triangle sw
                ChangeToTriange(_dirtMap[a - 1, b], NW_ROTATION);
            }
            if (a + 1 < Width && !_bitMap[a + 1, b + 1])
            {
                // replace right square with triangle se
                ChangeToTriange(_dirtMap[a + 1, b], NE_ROTATION);
            }
        }
        if (a > 0 && !_bitMap[a - 1, b])
        {
            if (b > 0 && !_bitMap[a - 1, b - 1])
            {
                //replace above square with triangle ne
                ChangeToTriange(_dirtMap[a, b - 1], NE_ROTATION);
            }
            if (b + 1 < Height && !_bitMap[a - 1, b + 1])
            {
                // replace bottom square with triangle se
                ChangeToTriange(_dirtMap[a, b + 1], SE_ROTATION);
            }
        }
        if (a + 1 < Width && !_bitMap[a + 1, b])
        {
            if (b > 0 && !_bitMap[a + 1, b - 1])
            {
                //replace above square with triangle ne
                ChangeToTriange(_dirtMap[a, b - 1], NW_ROTATION);
            }
            if (b + 1 < Height && !_bitMap[a + 1, b + 1])
            {
                // replace bottom square with triangle se
                ChangeToTriange(_dirtMap[a, b + 1], SW_ROTATION);
            }
        }*/
    }

    public void AddDirt(int a, int b)
    {
        _dirtMap[a, b].GetComponent<Collider2D>().enabled = true;
        _dirtMap[a, b].GetComponent<SpriteRenderer>().enabled = true;
        _bitMap[a, b] = true;
    }

    private void ChangeToTriange(GameObject dirt, float rotation)
    {
        dirt.GetComponent<SpriteRenderer>().sprite = DirtCornerSprite;
        dirt.transform.localEulerAngles = new Vector3(0, 0, rotation);
    }
}
