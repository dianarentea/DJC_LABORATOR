using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour
{
    public GameObject blackPiecePrefab; // Referința la prefab-ul piesei negre
    public GameObject whitePiecePrefab; // Referința la prefab-ul piesei albe
    public Transform piecesParent; // Parent pentru piese pentru a menține ierarhia organizată
    public float squareSize = 1.0f; // Presupunem că fiecare pătrat este de 1 unitate

    // Poziția inițială a colțului stâng jos al tablei de șah
    public Vector3 boardStart = new Vector3(-3.5f, 0.0f, -3.5f);

    void Start()
    {
        PlacePieces();
    }

    void PlacePieces()
    {
        // Offset pentru a centra piesele pe pătratele negre
        Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);

        for (int y = 0; y < 3; y++) // Primele 3 rânduri pentru piesele negre
        {
            for (int x = 0; x < 8; x++)
            {
                // Calcul poziției centrate pe căsuța de șah
                Vector3 blackPosition = boardStart + new Vector3(x, 0, y) * squareSize + pieceOffset;

                // Plasează piesele negre doar pe pătratele negre
                if ((x + y) % 2 != 0)
                {
                    PlacePiece(blackPosition, blackPiecePrefab);
                }
            }
        }

        for (int y = 5; y < 8; y++) // Ultimele 3 rânduri pentru piesele albe
        {
            for (int x = 0; x < 8; x++)
            {
                // Calcul poziției centrate pe căsuța de șah
                Vector3 whitePosition = boardStart + new Vector3(x, 0, y) * squareSize + pieceOffset;

                // Plasează piesele albe doar pe pătratele negre
                if ((x + y) % 2 != 0)
                {
                    PlacePiece(whitePosition, whitePiecePrefab);
                }
            }
        }
    }

    void PlacePiece(Vector3 position, GameObject piecePrefab)
    {
        // Creează o piesă și ajustează-i poziția
        GameObject newPiece = Instantiate(piecePrefab, position, Quaternion.identity);
        newPiece.transform.SetParent(piecesParent);

        // Ajustează poziția pe axa Y dacă este necesar
        newPiece.transform.localPosition += new Vector3(0, 0, 0);
    }
}
