var textures: Texture[];
private var rndNr:float;

function Awake () {

rndNr=Mathf.Floor(Random.value*textures.length);
GetComponent.<Renderer>().material.mainTexture=textures[rndNr];
}

function Update () {
}