using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class ItemGenerator
{
    public static CannonData Cannon(float level)
    {
        return new CannonData()
        {
            m_equipmentLevel = (int)level,
            m_baseDamage = StatCalculator.CalculateBaseDamage(level),
            m_baseFireRate = StatCalculator.CalculateBaseFireRate(level),
            m_damageType = StatCalculator.GetRandomValue<EnergyType>(),
            
        };
    }
    
}
