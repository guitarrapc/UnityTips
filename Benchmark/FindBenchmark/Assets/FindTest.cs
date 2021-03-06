﻿using System;
using UnityEngine;

// http://baba-s.hatenablog.com/entry/2014/07/09/093240
/// <summary>
/// Find 関数の処理速度を計測するスクリプト
/// </summary>
public sealed class FindTest : MonoBehaviour
{
    public int LoopCount = 1000; // 1 回のテストで Find 関数を実行する回数
    public int TestCount = 10;   // テストの回数
    public int NumGameObjects = 100;  // 検索対象のオブジェクトの数

    /// <summary>
    /// ゲーム開始時に指定された数分のオブジェクトを生成します
    /// </summary>
    private void Awake()
    {
        var types = new PrimitiveType[]
        {
            PrimitiveType.Capsule,
            PrimitiveType.Cube,
            PrimitiveType.Cylinder,
            PrimitiveType.Plane,
            PrimitiveType.Quad,
            PrimitiveType.Sphere,
        };

        for (int i = 0; i < NumGameObjects; i++)
        {
            GameObject.CreatePrimitive(types[UnityEngine.Random.Range(0, types.Length)]);
        }
    }

    /// <summary>
    /// Find, FindGameObjectWithTag, FindObjectOfType 関数を実行するボタンを表示します
    /// </summary>
    private void OnGUI()
    {
        DrawButton(0, "Find", () => GameObject.Find("Player"));
        DrawButton(100, "FindGameObjectWithTag", () => GameObject.FindGameObjectWithTag("Player"));
        DrawButton(200, "FindObjectOfType", () => GameObject.FindObjectOfType<BoxCollider>());
    }

    /// <summary>
    /// 指定された関数を実行するボタンを表示します
    /// </summary>
    private void DrawButton(float buttonY, string buttonText, Action findAct)
    {
        if (GUI.Button(new Rect(0, buttonY, Screen.width, 100), buttonText))
        {
            var sum = 0f;
            for (int i = 0; i < TestCount; i++)
            {
                var time = Time.realtimeSinceStartup;
                for (int j = 0; j < LoopCount; j++)
                {
                    findAct();
                }
                time = Time.realtimeSinceStartup - time;
                sum += time;
            }
            var avg = sum / TestCount;
            Debug.Log(buttonText + ":" + avg + "sec");
        }
    }
}