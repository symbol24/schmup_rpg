﻿using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public static class StatCalculator {
    enum ChassisSpeeds : int
    {
        verySlow = 2,
        slow = 5,
        average = 8,
        fast = 11,
        veryFast = 14
    };

    public static float CalculateBaseDamage(float level)
    {
        float min = ((level / 2) + 0.5f);
        float max = level + ((level / 2) + 1);
        float ret = Random.Range(min, max);
        ret = (float)System.Math.Round(ret, 2);
        return ret;
    }

    public static float CalculateBaseFireRate(float level)
    {
        float min = -((7 * level ) / 1980f) + (983f / 1980f);
        float max = -((7 * level ) / 1980f) + (785f/1980f);
        float ret = Random.Range(min, max);
        ret = (float)System.Math.Round(ret, 2);
        return ret;
    }

    public static float CalculateBaseHP(float level)
    {
        float max = 50 + ( level * (level + 1 )) / 2;
        float min = max * 0.90f;
        float ret = Random.Range(min, max);
        ret = (float)System.Math.Round(ret, 2);
        return ret;
    }

    public static float CalculateEAIBaseHP(float level)
    {
        float max = 5 + (level * (level + 1)) / 2;
        float min = max * 0.90f;
        float ret = Random.Range(min, max);
        ret = (float)System.Math.Round(ret, 2);
        return ret;
    }

    public static float CalculateBaseArmor(float level)
    {
        float max = 1 + (level * (level + 1)) / 2;
        float min = max * 0.90f;
        float ret = Random.Range(min, max);
        ret = (float)System.Math.Round(ret, 2);
        return ret;
    }

    public static float CalculateBaseShield(float level)
    {
        float max = 20 + (level * (level + 1)) / 2;
        float min = max * 0.90f;
        float ret = Random.Range(min, max);
        ret = (float)System.Math.Round(ret, 2);
        return ret;
    }

    public static float CalculateBaseSpeed(float level, ChassisController.ChassisSize chassisSize)
    {
        float ret = 0;
        int min = 0;
        int max = 5;


        switch (chassisSize)
        {
            case ChassisController.ChassisSize.large:
                min = 0;
                max = 2;
                break;

            case ChassisController.ChassisSize.medium:
                min = 1;
                max = 4;
                break;

            case ChassisController.ChassisSize.small:
                min = 2;
                max = 5;
                break;
        }

        ret = (int)GetRandomValue<ChassisSpeeds>(min, max);

        return ret;
    }

    public static T GetRandomValue<T>(int min, int max)
    {

        T[] values = (T[])(System.Enum.GetValues(typeof(T)));

        if (min == 0 && max == 0) {
            return values[Random.Range(0, values.Length)];
        }
        else
        {
            return values[Random.Range(min, max)];
        }
    }

    public static T GetRandomValue<T>()
    {
        if (typeof (T).IsEnum)
        {
            return GetRandomValue<T>(0, Enum.GetValues(typeof (T)).Length);
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    public static int GetCurrentLevel(float exp)
    {
        return (int)(exp / 100);
    }

    public static float GetExpValue(EnemyController.EnemyType type)
    {
        float ret = 0.0f;
        switch (type)
        {
            case EnemyController.EnemyType.grunt:
                ret = 1.0f;
                break;
            case EnemyController.EnemyType.miniboss:
                ret = 20.0f;
                break;
            case EnemyController.EnemyType.boss:
                ret = 50.0f;
                break;
        }

        return ret;
    }

}
