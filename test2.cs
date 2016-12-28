using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

public class test2 : MonoBehaviour {
	public Text debug;
	public Text lenght;
	public Text stat;
	public InputField input;
	public Image loadingBar;
	public AudioSource audioSource;
	public string[] url;

	public WWW request;
	public WebClient webClient;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		//debug.text = Application.persistentDataPath.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (request.progress.ToString());
		if(request != null)
		Debug.Log( request.bytesDownloaded);
	}
	public void OtherSource(){
		debug.text = "";
		StartCoroutine (DownloadAudio());
	}
	public void GoogleTranslate(){
		debug.text = "";
		StartCoroutine (DownloadAudio2());
	}
	public void PersistainFolder(){
		debug.text = "";
		StartCoroutine (DownloadAudio3());
	}
	public void StreamingAssetsFolder(){
		debug.text = "";
		StartCoroutine (DownloadAudio4());
	}
	public void VoiceRSS(){
		debug.text = "";
		StartCoroutine (DownloadAudio5(input.text));
	}
	public void stopPlay(){
		stat.text = "";
		debug.text = "";
		lenght.text = "";
		audioSource.Stop ();
	}
	IEnumerator DownloadAudio(){
		request = new WWW (url[0]);
		yield return request;
		if (request.error != null) {
			debug.text = request.error;
		} else {
			audioSource.clip = request.GetAudioClip (false, true, AudioType.MPEG);
			audioSource.Play ();
			debug.text = url[0];
			Debug.Log( request.bytesDownloaded);
			lenght.text = request.bytes.Length.ToString();
			//Debug.Log("byte.lenght "+request.bytes.Length);
			//Debug.Log("Progress "+request.progress);
			//Debug.Log("byteDownloaded "+request.bytesDownloaded);
			//Debug.Log(float.Parse (request.bytesDownloaded.ToString())/ float.Parse( request.bytes.ToString())*100);
			//Debug.Log("ukuran file "+(float)request.size);
			//Debug.Log("ukuran file yang udah kedownload"+(float)request.bytesDownloaded);
			if(request.size == request.bytesDownloaded){
				stat.text = "Download is complete";
				StartCoroutine (SaveAudio ());
			}else{
				stat.text = "Download in progress";
			}
		}
		
	}
	IEnumerator DownloadAudio2(){
		Debug.Log (url);
		request = new WWW (url[1]);
		yield return request;
		if (request.error != null) {
			stat.text = request.error;
		} else {
			audioSource.clip = request.GetAudioClip (false, true, AudioType.MPEG);
			audioSource.Play ();
			debug.text = url[1];
			Debug.Log( request.bytesDownloaded);
			lenght.text = request.bytes.Length.ToString();
			if(request.size == request.bytesDownloaded){
				stat.text = "Download is complete";
				StartCoroutine (SaveAudio2 ());
			}else{
				stat.text = "Download in progress";
			}
		}
		
	}
	IEnumerator DownloadAudio3(){
		string x = "file://" + Application.persistentDataPath + "/_song.mp3";
		Debug.Log (x);
		request = new WWW (x);
		yield return request;
		if (request.error != null) {
			debug.text = request.text;
			stat.text = request.error;
		} else {
			audioSource.clip = request.GetAudioClip (false, true, AudioType.MPEG);
			audioSource.Play ();
			debug.text = x;
			Debug.Log( request.bytesDownloaded);
			lenght.text = request.bytes.Length.ToString();
			if(request.size == request.bytesDownloaded){
				stat.text = "Download is complete";
				StartCoroutine (SaveAudio2 ());
			}else{
				stat.text = "Download in progress";
			}
		}
		
	}
	IEnumerator DownloadAudio4(){
		string x = "file://" + Application.streamingAssetsPath + "/_song.mp3";
		Debug.Log (x);
		request = new WWW (x);
		yield return request;
		if (request.error != null) {
			debug.text = request.text;
			stat.text = request.error;
		} else {
			audioSource.clip = request.GetAudioClip (false, true, AudioType.MPEG);
			audioSource.Play ();
			debug.text = x;
			Debug.Log (request.bytesDownloaded);
			lenght.text = request.bytes.Length.ToString ();
			if (request.size == request.bytesDownloaded) {
				stat.text = "Download is complete";
			} else {
				stat.text = "Download in progress";
			}
		}
	}
		
	IEnumerator DownloadAudio5(string words){
		// Remove the "spaces" in excess
		Regex rgx = new Regex ("\\s+");
		// Replace the "spaces" with "% 20" for the link Can be interpreted
		string result = rgx.Replace (words, "+");
			string x = "http://api.voicerss.org/?key=451a1fe169e3464c86de603452f0505e&hl=en-gb&src="+result+"&c=MP3&f=16khz_8bit_stereo&r=2";
			Debug.Log (x);
			request = new WWW (x);
			yield return request;
			if (request.error != null) {
				debug.text = request.text;
				stat.text = request.error;
			} else {
				audioSource.clip = request.GetAudioClip (false, true, AudioType.MPEG);
				audioSource.Play ();
				debug.text = x;
				Debug.Log( request.bytesDownloaded);
				lenght.text = request.bytes.Length.ToString();
				if(request.size == request.bytesDownloaded){
				stat.text = "Download is complete";
				StartCoroutine (SaveAudio ());
				}else{
					stat.text = "Download in progress";
				}
			}

	}
	
	IEnumerator SaveAudio(){
		lenght.text = request.bytes.Length.ToString();
		File.WriteAllBytes(Application.persistentDataPath + "/_song.mp3", request.bytes);
		Debug.Log (Application.persistentDataPath);
		
		yield return null;
	}
	IEnumerator SaveAudio2(){
		lenght.text = request.bytes.Length.ToString();
		File.WriteAllBytes(Application.streamingAssetsPath + "/_song.mp3", request.bytes);
		Debug.Log (Application.persistentDataPath);
		
		yield return null;
	}
}
