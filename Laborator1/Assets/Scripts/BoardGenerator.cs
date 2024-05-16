using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BoardGenerator : MonoBehaviour
{
    public GameObject _whiteField; 
    public GameObject _blackField;
    public GameObject _whitePiecePrefab;
    public GameObject _blackPiecePrefab;

    public Material _selectionMaterial;
    public GameObject selectedPiece;
    public Material originalMaterial;
    public TextMeshPro scoreText;

    public Vector3 boardStart = new Vector3(0, 0, 0);

    private string currentPlayer = "White";
    private int whiteScore = 0;
    private int blackScore = 0;

    void Start()
    {
        currentPlayer = "White";
        InitializeBoard();
        PlacePieces();
    }
    public void SelectPiece(GameObject piece)
    {
        Debug.Log("Piece selected: " + piece.tag);
        Debug.Log("Current player: " + currentPlayer);
        if ((currentPlayer == "White" && piece.CompareTag("WhitePiece")) || (currentPlayer == "Black" && piece.CompareTag("BlackPiece")))
            {      
            if (selectedPiece == piece)
            {
                DeselectPiece();
                return;
            }

            if (selectedPiece != null)
            {
                DeselectPiece();
            }

            selectedPiece = piece;
            originalMaterial = piece.GetComponent<Renderer>().material;
            piece.GetComponent<Renderer>().material = _selectionMaterial;
            }
    }

    public void DeselectPiece()
    {
        if (selectedPiece != null)
        {
            selectedPiece.GetComponent<Renderer>().material = originalMaterial;
            selectedPiece = null;
            originalMaterial = null;
        }
    }

    public void MoveSelectedPiece(Vector3 newPosition)
    {
        if (selectedPiece != null)
        {
            Vector3 currentPosition = selectedPiece.transform.position;
            Vector3 difference = newPosition - currentPosition;

            Debug.Log("Moving piece to: " + newPosition);
            Debug.Log("Current position: " + currentPosition);
            Debug.Log("Difference: " + Mathf.Round(difference.x) + Mathf.Round(difference.z));

            if (Mathf.Round(difference.x) == 0 || Mathf.Round(difference.z) == 0)
            {
                Debug.Log("Invalid move!");    
                scoreText.text = "Invalid move! Only diagonal!";
            }
            else if (Math.Abs(Mathf.Round(difference.x)) == 5 && Math.Abs(Mathf.Round(difference.z)) == 5)
            {
                scoreText.text = "Normal piece move";
                selectedPiece.transform.position = newPosition;
                DeselectPiece();
                ChangeTurn();
            }
            else if(Math.Abs(Mathf.Round(difference.x)) == 10 && Math.Abs(Mathf.Round(difference.z))== 10)
            {
                Vector3 midPosition = currentPosition + new Vector3(difference.x / 2, 0, difference.z / 2);
                Collider[] colliders = Physics.OverlapSphere(midPosition, 0.1f);
                bool hasMiddlePiece = false;

                foreach (Collider collider in colliders)
                {
                    if (currentPlayer == "White" && collider.gameObject.CompareTag("BlackPiece"))
                    {
                        Destroy(collider.gameObject); // Capture the black piece
                        hasMiddlePiece = true;
                        whiteScore++;
                    }
                    else if (currentPlayer == "Black" && collider.gameObject.CompareTag("WhitePiece"))
                    {
                        Destroy(collider.gameObject); // Capture the white piece
                        hasMiddlePiece = true;
                        blackScore++;
                    }
                }
                if (hasMiddlePiece)
                {
                    scoreText.text = "Capturing piece move";
                    selectedPiece.transform.position = newPosition;
                    DeselectPiece();
                    ChangeTurn();
                }
                else
                {
                    scoreText.text = "Can't move 2 pos as long as you don't capture a piece";
                }
            }
            else
            {
                Debug.Log("Invalid move. Can only move one square diagonally or capture by jumping over a piece.");
            }

        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("WhitePiece") || hit.collider.gameObject.CompareTag("BlackPiece"))
                {
                    Debug.Log("Piece selected");
                    SelectPiece(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.CompareTag("Field") && selectedPiece != null)
                {
                    Debug.Log("Field selected");
                    Vector3 newPosition = hit.collider.gameObject.transform.position + new Vector3(0, 0.5f, 0);
                    MoveSelectedPiece(newPosition);
                }

                CheckWinCondition();
            }
        }
    }
    private void ChangeTurn()
    {
        currentPlayer = (currentPlayer == "White") ? "Black" : "White";
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"White: {whiteScore}\nBlack: {blackScore}\nTurn: {currentPlayer}";
    }

    private void CheckWinCondition()
    {    
        if (whiteScore == 12)
        {
            scoreText.text = "White won!";
        }
        else if (blackScore == 12)
        {
            scoreText.text = "Black won!";
        }
    }

    private void InitializeBoard()
    {
        for(int x=0; x<=35; x+=5)
        {
            for(int z=0; z<=35; z+=5)
            {
                Vector3 fieldPosition = boardStart + new Vector3(x, 0, z);
                if ((x + z) % 2 == 0)
                {
                    PlaceField(fieldPosition, _whiteField);
                }
                else
                {
                    PlaceField(fieldPosition, _blackField);
                }
            }
        }
    }
    private void PlacePieces()
    {

        for (int x = 0; x <= 10; x += 5)
        {
            for (int z = 0; z <= 35; z += 5)
            {
                Vector3 piecePosition = boardStart + new Vector3(x, 0, z);

                if ((x + z) % 2 != 0)
                {
                    PlacePiece(piecePosition, _whitePiecePrefab, "WhitePiece");
                }
            }
        }


        for (int x = 25; x <= 35; x += 5)
        {
            for (int z = 0; z <= 35; z += 5)
            {
                Vector3 piecePosition = boardStart + new Vector3(x, 0, z);

                if ((x + z) % 2 != 0)
                {
                    PlacePiece(piecePosition, _blackPiecePrefab, "BlackPiece");
                }
            }
        }
    }
    private void PlaceField(Vector3 position, GameObject field)
    {
        GameObject newField = Instantiate(field, position, Quaternion.identity);
        newField.tag = "Field";
        newField.transform.localPosition += new Vector3(0, 0, 0);
        if (newField.GetComponent<Collider>() == null)
        {
            newField.AddComponent<BoxCollider>();
        }
    }
    private void PlacePiece(Vector3 position, GameObject piece, string tag)
    {
        GameObject newPiece = Instantiate(piece, position, Quaternion.identity);
        newPiece.tag = tag;
        newPiece.transform.localPosition += new Vector3(0, 0, 0);
        if (newPiece.GetComponent<Collider>() == null)
        {
            newPiece.AddComponent<BoxCollider>();
        }
        if (newPiece.GetComponent<Renderer>() == null)
        {
            newPiece.AddComponent<MeshRenderer>();
            newPiece.AddComponent<MeshFilter>();
        }
    }
}
