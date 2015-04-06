using UnityEngine;
using System.Collections;

public class DamageCalculators : MonoBehaviour {
	public static int proton = 0;
	public static int photon = 1;
	public static int plasma = 2;
	public static float bonusAtt = 0.25f;

	public static float ShieldHit(float damage, float hp, float armor, int bulletType, int shieldType){
		float modifer = 0.0f;
		if ((bulletType == proton && shieldType == photon) || (bulletType == photon && shieldType == plasma) || (bulletType == plasma && shieldType == proton))
			modifer = bonusAtt;
		else if ((bulletType == photon && shieldType == proton) || (bulletType == plasma && shieldType == photon) || (bulletType == proton && shieldType == plasma))
			modifer = -bonusAtt;

		damage += damage * modifer;

		return Hit (damage, hp, armor);
	}

	//hit and mitigate damage together yay!
	public static float Hit(float damage, float hp, float armor) {
		if(damage > armor){
			damage -= armor;
		}else{
			damage = 0;
		}
		if (hp - damage <= 0)
			return 0;

		return hp - damage;
	}
}
