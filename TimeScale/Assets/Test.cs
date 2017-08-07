using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
//		ParticleSystem ps = null;
//		ps.Simulate();
//		Animation s = null;
//		s.Sample();
//		Time.timeScale
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	


	/// <summary>
	/// 模拟动画时间轴
	/// </summary>
	/// <param name="anim"></param>
	/// <param name="clipName"></param>
	/// <returns></returns>
	private IEnumerator SimulateAnimationTimeline(Animation anim, string clipName)
	{
		//获取到动画状态组件
		AnimationState animState = anim[clipName];
		if (!animState)
		{
			Debug.LogErrorFormat("can't get clip -> {0} from animation -> {1}", clipName, anim.name);
			yield break;
		}
		animState.normalizedTime = 0f;
		float timer = 0f;
		//随着动画时间轴的行进，归一化动画时间会逐步增加
		while (animState.normalizedTime < 1f)
		{
			//动画取样
			anim.Sample();
			//持续未缩放时间片段
			yield return Time.unscaledDeltaTime;
			timer += Time.unscaledDeltaTime;;
			animState.normalizedTime += timer/anim.clip.length;
		}
	}
	
	/// <summary>
	/// 模拟粒子时间轴
	/// </summary>
	/// <param name="particleGo"></param>
	/// <param name="timePeriod"></param>
	private void SimulateParticleTimeline(GameObject particleGo, float timePeriod)
	{
		ParticleSystem[] allPS = particleGo.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < allPS.Length; i++)
		{
			var ps = allPS[i];
			ps.Simulate(timePeriod, false, true, false);
		}
	}
	
}
