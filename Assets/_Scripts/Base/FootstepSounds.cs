using UnityEngine;
using System.Collections;

public class FootstepSounds : MonoBehaviour
{

    

    public TextureType[] textureTypes;

    public AudioSource audioS;

    SoundController SController;

    string AnimationState;

    // Use this for initialization
    void Start()
    {
        GameObject check = GameObject.FindGameObjectWithTag("Sound Controller");

        if (check != null)
        {
            SController = check.GetComponent<SoundController>();

        }
    }

    

   
    void PlayFootstepSound(string State)
    {
        AnimationState=State;

        RaycastHit hit;
        Vector3 start = transform.position + transform.up;
        Vector3 dir = Vector3.down;

        if (Physics.Raycast(start, dir, out hit, 1.3f))
        {
            if (hit.collider.GetComponent<MeshRenderer>())
            {
                PlayMeshSound(hit.collider.GetComponent<MeshRenderer>());
            }
            else if (hit.collider.GetComponent<Terrain>())
            {
                PlayTerrainSound(hit.collider.GetComponent<Terrain>(), hit.point);
            }
        }
    }

    void PlayMeshSound(MeshRenderer renderer)
    {

        if (audioS == null)
        {
            Debug.LogError("PlayMeshSound -- We have no audio source to play the sound from.");
            return;
        }

        if (SController == null)
        {
            Debug.LogError("PlayMeshSound -- No sound manager!!!");
            return;
        }

                if (textureTypes.Length > 0)
        {


            if(AnimationState==null)
                    return;


            foreach (TextureType type in textureTypes)
            {

               
                if (renderer.material.mainTexture == type.texture)
                {
    
    
                    if(AnimationState==null)
                        return;
    
                    switch (AnimationState)
                    {
    
                        case "Walk":
    
                        if(type.Walkfootstep==null)
                          return;
                        
                        SController.PlaySound(audioS, type.Walkfootstep,0.5f, true, 1, 1.2f);
    
                        break;
    
    
                        case "Run":
    
                        if(type.Runfootstep==null)
                          return;
                        
                        SController.PlaySound(audioS, type.Runfootstep, 0.5f,true, 1, 1.2f);
    
                        break;
    
                        case "Crouch":
    
                        if(type.Crouchfootstep==null)
                          return;
                        
                        SController.PlaySound(audioS, type.Crouchfootstep, 0.2f ,true, 1, 1.2f);
    
                        break;
    
    
                        case "Land":
    
                        if(type.Landfootstep==null)
                          return;
                        
                        SController.PlaySound(audioS, type.Landfootstep, 0.5f,true, 1, 1.2f);
    
                        break;
    
                        case "Jump":
    
                        if(type.Jumpfootstep==null)
                          return;
                        
                        SController.PlaySound(audioS, type.Jumpfootstep, 0.5f,true, 1, 1.2f);
    
                        break;
    
                        default:
                        return;
    
                    }
                        
                }    
    
            }
/* 
        if (textureTypes.Length > 0)
        {
            foreach (TextureType type in textureTypes)
            {

                if (type.footstepSounds.Length == 0)
                {
                    return;
                }

                foreach (Texture tex in type.textures)
                {
                    if (renderer.material.mainTexture == tex)
                    {
                        SController.PlaySound(audioS, type.footstepSounds[Random.Range(0, type.footstepSounds.Length)], true, 1, 1.2f);
                    }
                }
            }
        }
    */
    }
}

    void PlayTerrainSound(Terrain t, Vector3 hitPoint)
    {
        if (audioS == null)
        {
            Debug.LogError("PlayTerrainSound -- We have no audio source to play the sound from.");
            return;
        }

        if (SController == null)
        {
            Debug.LogError("PlayTerrainSound -- No sound manager!!!");
            return;
        }

        if (textureTypes.Length > 0)
        {

            int textureIndex = TerrainSurfaceDetector.GetMainTexture(hitPoint);

            if(AnimationState==null)
                    return;


            foreach (TextureType type in textureTypes)
            {

               
                    if (t.terrainData.splatPrototypes[textureIndex].texture == type.texture)
                    {


                            if(AnimationState==null)
                                 return;

                            switch (AnimationState)
                            {
                              
                                case "Walk":
              
                                if(type.Walkfootstep==null)
                                  return;
                                
                                SController.PlaySound(audioS, type.Walkfootstep, 0.3f,true, 1, 1.2f);
              
                                break;
              
              
                                case "Run":
              
                                if(type.Runfootstep==null)
                                  return;
                                
                                SController.PlaySound(audioS, type.Runfootstep, 0.5f,true, 1, 1.2f);
              
                                break;
              
                                case "Crouch":
              
                                if(type.Crouchfootstep==null)
                                  return;
                                
                                SController.PlaySound(audioS, type.Crouchfootstep,0.1f, true, 1, 1.2f);
              
                                break;
              
              
                                case "Land":
              
                                if(type.Landfootstep==null)
                                  return;
                                
                                SController.PlaySound(audioS, type.Landfootstep, 0.2f,true, 1, 1.2f);
              
                                break;
              
                                case "Jump":
              
                                if(type.Jumpfootstep==null)
                                  return;
                                
                                SController.PlaySound(audioS, type.Jumpfootstep, 0.2f,true, 1, 1.2f);
              
                                break;
              
                                default:
                                return;
              
                            }
                                
                        }

            }
}
/* 
            foreach (TextureType type in textureTypes)
            {

                if (type.footstepSounds.Length == 0)
                {
                    return;
                }

                foreach (Texture tex in type.textures)
                {
                    if (t.terrainData.splatPrototypes[textureIndex].texture == tex)
                    {
                        SController.PlaySound(audioS, type.footstepSounds[Random.Range(0, type.footstepSounds.Length)], true, 1, 1.2f);
                    }
                }
            }

*/
        
    
 }

}




[System.Serializable]
public class TextureType
{
    public string name;
   // public Texture[] textures;
    public Texture texture;
    
    //public AudioClip[] footstepSounds;

    public AudioClip  Walkfootstep ;
    public AudioClip  Runfootstep ;
    public AudioClip  Crouchfootstep ;
    public AudioClip  Jumpfootstep ;
    public AudioClip  Landfootstep ;
   
}



// Walk  -   Run  - Crouch  - Land  - Jump

