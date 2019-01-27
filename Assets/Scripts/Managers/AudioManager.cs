
using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Managers
{
	public class AudioManager : MonoBehaviour
	{

		#region Singleton
		public static AudioManager Instance;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
				DontDestroyOnLoad(gameObject);
			}
		}
		#endregion


		/// <summary>
		/// Come Gameobject usa l'AudioManager
		/// </summary>
		public static EventInstance PlayAudio(string eventRef)
		{
			return PlayAudio(eventRef, Instance.gameObject);
		}

		/// <summary>
		/// Per gli eventi audio che possono anche stopparsi e avere cambi di parametro
		/// </summary>
		public static EventInstance PlayAudio(string eventRef, GameObject obj)
		{
			EventInstance audioInstance;
			try
			{
				audioInstance = RuntimeManager.CreateInstance(eventRef);

			}
			catch (EventNotFoundException e)
			{
				//Riproduco evento dummy quando non trovo eventRef tra quelli di FMOD perche non riusciamo a gestire l'eccezione altrimenti
				audioInstance = RuntimeManager.CreateInstance("");
				Debug.LogError($"[AudioManager] Cant find this eventRef:{eventRef}" + e);
			}

			RuntimeManager.AttachInstanceToGameObject(audioInstance, obj.transform, (Rigidbody) null);
			audioInstance.start();

			return audioInstance;
		}
		
		
		/// <summary>
		/// Per gli eventi audio che possono anche stopparsi e avere cambi di parametro
		/// </summary>
		public static EventInstance CreateInstance(string eventRef, GameObject obj)
		{
			EventInstance audioInstance;
			try
			{
				audioInstance = RuntimeManager.CreateInstance(eventRef);

			}
			catch (EventNotFoundException e)
			{
				//Riproduco evento dummy quando non trovo eventRef tra quelli di FMOD perche non riusciamo a gestire l'eccezione altrimenti
				audioInstance = RuntimeManager.CreateInstance("");
				Debug.LogError($"[AudioManager] Cant find this eventRef:{eventRef}" + e);
			}

			RuntimeManager.AttachInstanceToGameObject(audioInstance, obj.transform, (Rigidbody) null);

			return audioInstance;
		}
		
		
		/// <summary>
		/// Come Gameobject usa l'AudioManager
		/// </summary>
		public static void PlayOneShotAudio(string eventRef)
		{
			PlayOneShotAudio(eventRef, Instance.gameObject);
		}
		
		
		/// <summary>
		/// Per gli eventi audio che vengono triggerati, suonano fino alla fine e muoiono da soli
		/// </summary>
		public static void PlayOneShotAudio(string eventRef, GameObject obj)
		{
			RuntimeManager.PlayOneShotAttached(eventRef, obj);
		}

		/// <summary>
		/// Passando l'istanza si puo chiamare la stop di un suono
		/// di default c'è un fade out ma è possibile bloccare il suono immediatamente
		/// </summary>
		public static void StopAudio(EventInstance audioInstance, bool immediately = false)
		{
			if (!audioInstance.isValid()) return;
			audioInstance.stop(immediately ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			audioInstance.release();
		}

		/// <summary>
		/// Setta il parametro sull'istanza che viene passata
		/// </summary>
		public static void SetParameterToInstance(EventInstance audioInstance, string parameterName,
			float parameterValue)
		{
			if(!audioInstance.isValid()) return;
			audioInstance.setParameterValue(parameterName, parameterValue);
		}

		/// <summary>
		/// Restituisce true se l'instance audio sta ancora suonando
		/// </summary>
		public static bool IsPlaying(EventInstance audioInstance)
		{
			if (!audioInstance.isValid()) return false;

			PLAYBACK_STATE playbackState;
			audioInstance.getPlaybackState(out playbackState);
			return (playbackState != PLAYBACK_STATE.STOPPED);
		}

		/// <summary>
		/// Coroutine che dice restituisce callback quando un evento audio ha finito di suonare
		/// </summary>
		public static IEnumerator WaitForAudioFinishedCoroutine(EventInstance audio, Action callback = null)
		{
			yield return new WaitWhile(() => IsPlaying(audio));
			callback?.Invoke();
		}

		/// <summary>
		/// Stoppa tutti i suoni con fade finale
		/// </summary>
		public static void StopAllSounds(bool immediately = false)
		{
			RuntimeManager.GetBus("bus:/").stopAllEvents(immediately ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}
		
		
	}
}