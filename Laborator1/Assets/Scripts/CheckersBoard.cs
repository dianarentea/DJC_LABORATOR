using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour
{
    public GameObject blackPiecePrefab; 
    public GameObject whitePiecePrefab; 
    public float squareSize = 1.0f;

    public Vector3 boardStart = new Vector3(-3.5f, 0.0f, -3.5f);

    void Start()
    {
        PlacePieces();
    }

    void PlacePieces()
    {
        Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);

        for (int y = 0; y < 3; y++) 
        {
            for (int x = 0; x < 8; x++)
            {
              
                Vector3 blackPosition = boardStart + new Vector3(x, 0, y) * squareSize + pieceOffset;

                if ((x + y) % 2 != 0)
                {
                    PlacePiece(blackPosition, blackPiecePrefab);
                }
            }
        }

        for (int y = 5; y < 8; y++) 
        {
            for (int x = 0; x < 8; x++)
            {
                Vector3 whitePosition = boardStart + new Vector3(x, 0, y) * squareSize + pieceOffset;

                if ((x + y) % 2 != 0)
                {
                    PlacePiece(whitePosition, whitePiecePrefab);
                }
            }
        }
    }

    void PlacePiece(Vector3 position, GameObject piecePrefab)
    {
        GameObject newPiece = Instantiate(piecePrefab, position, Quaternion.identity);

        newPiece.transform.localPosition += new Vector3(0, 0, 0);
    }
}
