using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//              Берик Алишер
// Код по манипулированию объектов из массива
// Код позволяет:
// 1) Двигать объекты по
//    * оси X (A и D, или стрелки)
//    * оси Y (Space и LeftControl)
//    * оси Z (W и S, или стрелки)
// 2) Вращать объекты (кнопка R)
// 3) Изменять размер (+ и - на Numpade)
// 4) Поставить кубики в случайную позицию (Кнопка G)

// ---------------------------------------------
// Про типы данных
// 1) Целочисленные:
// Тип byte (8 бит) & short (16 бит) - можно использовать в целях экономии памяти
// * byte - беззнаковый байт                      (0 : 255)        | (0 : 2^8 - 1)
// * sbyte - signed byte - знаковый байт          (-128 : 127)     | ((-2^7) : (2^7)-1)
// * short - знаковый short                       (-32768 : 32767) | ((-2^15) : (2^15)-1)
// * ushort - unsigned short - беззнаковый short  (0 : 65535)      | (0 : (2^16)-1)
//
// * Тип int & uint (оба 32 бит) - для обычных целочисленных переменных
// * Тип long & ulong (оба 64 бит) - для огромных чисел
// 
// 2) Числа с плавующей точкой
// * Тип float (32 бит) - для обычных простых вычислении (внизу использована)
// * Тип double (64 бит) - для более точных вычислении

// * Тип decimal (128 бит, 16 байт) - до 28-29 цифр после запятой
// Где требуется практически абсолютное точное значение
// Например, физические и математические вычисления
// ---------------------------------------------

public class Affecter : MonoBehaviour
{
    private float horizontal; // Для оси Х (лево и право)
    private float vertical; // Для оси Z (вперёд и назад)
                            // Для оси Y используется клавиши Space и LeftControl


    // _Speed - скорость по оси _
    public int xSpeed = 20;  // Поставил 20 - не слишком медленно и не слишком быстро
    public int ySpeed = 20;
    public int zSpeed = 20;

    // Список (массив) кубов
    public GameObject[] objects;


    void Update()
    {
        // Я обновляю каждый раз эти переменные
        // Чтобы достать новые значения для движения
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Дебаг в консоль
        Debug.Log("Горизонталь: " + horizontal + ", Вертикаль: " + vertical);

        // Ось Y (Space & LeftControl)
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

        // Ось X
        if (horizontal != 0)
        {
            foreach (GameObject obj in objects)
            {
                // Создаю новый вектор только по Х оси
                // Т.к. position принимает только вектора
                // По каждой оси только Read-Only значения
                // Тоже самое и с осью Z
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

        // Ось Z
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

        // Поворот
        if (Input.GetKey(KeyCode.R))
        {
            foreach (GameObject obj in objects)
            {
                obj.transform.Rotate(2f, 0, 0);
            }
        }

        // Размер
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

        // Рандомизация позиции
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (GameObject obj in objects)
            {
                obj.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            }
        }
    }

}
