using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//              ����� ������
// ��� �� ��������������� �������� �� �������
// ��� ���������:
// 1) ������� ������� ��
//    * ��� X (A � D, ��� �������)
//    * ��� Y (Space � LeftControl)
//    * ��� Z (W � S, ��� �������)
// 2) ������� ������� (������ R)
// 3) �������� ������ (+ � - �� Numpade)
// 4) ��������� ������ � ��������� ������� (������ G)

// ---------------------------------------------
// ��� ���� ������
// 1) �������������:
// ��� byte (8 ���) & short (16 ���) - ����� ������������ � ����� �������� ������
// * byte - ����������� ����                      (0 : 255)        | (0 : 2^8 - 1)
// * sbyte - signed byte - �������� ����          (-128 : 127)     | ((-2^7) : (2^7)-1)
// * short - �������� short                       (-32768 : 32767) | ((-2^15) : (2^15)-1)
// * ushort - unsigned short - ����������� short  (0 : 65535)      | (0 : (2^16)-1)
//
// * ��� int & uint (��� 32 ���) - ��� ������� ������������� ����������
// * ��� long & ulong (��� 64 ���) - ��� �������� �����
// 
// 2) ����� � ��������� ������
// * ��� float (32 ���) - ��� ������� ������� ���������� (����� ������������)
// * ��� double (64 ���) - ��� ����� ������ ����������

// * ��� decimal (128 ���, 16 ����) - �� 28-29 ���� ����� �������
// ��� ��������� ����������� ���������� ������ ��������
// ��������, ���������� � �������������� ����������
// ---------------------------------------------

public class Affecter : MonoBehaviour
{
    private float horizontal; // ��� ��� � (���� � �����)
    private float vertical; // ��� ��� Z (����� � �����)
                            // ��� ��� Y ������������ ������� Space � LeftControl


    // _Speed - �������� �� ��� _
    public int xSpeed = 20;  // �������� 20 - �� ������� �������� � �� ������� ������
    public int ySpeed = 20;
    public int zSpeed = 20;

    // ������ (������) �����
    public GameObject[] objects;


    void Update()
    {
        // � �������� ������ ��� ��� ����������
        // ����� ������� ����� �������� ��� ��������
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // ����� � �������
        Debug.Log("�����������: " + horizontal + ", ���������: " + vertical);

        // ��� Y (Space & LeftControl)
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (GameObject obj in objects)
            {
                obj.transform.position += Vector3.up * ySpeed * Time.deltaTime;

                if (obj.transform.position.y >= 5)
                {
                    Vector3 PostZ = new Vector3(obj.transform.position.x, 5, obj.transform.position.z);
                    obj.transform.position = PostZ;
                }

            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            foreach (GameObject obj in objects)
            {
                obj.transform.position -= Vector3.up * ySpeed * Time.deltaTime;

                if (obj.transform.position.y <= -5)
                {
                    Vector3 PostZ = new Vector3(obj.transform.position.x, -5, obj.transform.position.z);
                    obj.transform.position = PostZ;
                }
            }
        }

        // ��� X
        if (horizontal != 0)
        {
            foreach (GameObject obj in objects)
            {
                // ������ ����� ������ ������ �� � ���
                // �.�. position ��������� ������ �������
                // �� ������ ��� ������ Read-Only ��������
                // ���� ����� � � ���� Z
                Vector3 newPostX = new Vector3(xSpeed * horizontal * Time.deltaTime, 0, 0);
                obj.transform.position += newPostX;

                if (obj.transform.position.x >= 5)
                {
                    Vector3 PostZ = new Vector3(5, obj.transform.position.y, obj.transform.position.z);
                    obj.transform.position = PostZ;
                }
                else if (obj.transform.position.x <= -5)
                {
                    Vector3 PostZ = new Vector3(-5, obj.transform.position.y, obj.transform.position.z);
                    obj.transform.position = PostZ;
                }
            }
        }

        // ��� Z
        if (vertical != 0)
        {
            foreach (GameObject obj in objects)
            {
                Vector3 newPostZ = new Vector3(0, 0, zSpeed * vertical * Time.deltaTime);
                obj.transform.position += newPostZ;
                if (obj.transform.position.z >= 5)
                {
                    Vector3 PostZ = new Vector3(obj.transform.position.x, obj.transform.position.y, 5);
                    obj.transform.position = PostZ;
                }
                else if (obj.transform.position.z <= -5)
                {
                    Vector3 PostZ = new Vector3(obj.transform.position.x, obj.transform.position.y, -5);
                    obj.transform.position = PostZ;
                }
            }
        }

        // �������
        if (Input.GetKey(KeyCode.R))
        {
            foreach (GameObject obj in objects)
            {
                obj.transform.Rotate(2f, 0, 0);
            }
        }

        // ������
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            foreach (GameObject obj in objects)
            {
                obj.transform.localScale += new Vector3(.1f, .1f, .1f);
            }
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            foreach (GameObject obj in objects)
            {
                obj.transform.localScale -= new Vector3(.1f, .1f, .1f);
            }
        }

        // ������������ �������
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (GameObject obj in objects)
            {
                obj.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            }
        }
    }

}
