using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunsMinigameScript : Photon.MonoBehaviour
{
    public enum GameStateId
    {
        Initialize = 0,
        PrepareToStart,
        PrepareToStart3,
        PrepareToStart2,
        PrepareToStart1,
        PrepareToStartGO,
        Countdown,
        Playing,
        Paused,
        Completed,
        PrepareToExit,
        WaitForPlayersToConnect,
    }

    //public GameObject GunPrefab;
    public string[] BulletPrefab;
    public string[] Barrels;
    public string[] EnemyPrefabs;
    public string[] SpecialBarrels;
    public string[] BulletSplats;

    public UITexture MeterBar;
    //public UISprite MeterBarBackground;
    public List<string> CurrentBullets = new List<string>();
    private GameStateId State;
    private float AmmoTimer;
    private float NextBarrelSpawnTimer;
    public List<GameObject> SpawnedObjects = new List<GameObject>();
    public Texture2D[] SlimeTextures;
    public Texture2D[] SlimeLevel2Textures;
    public Color[] SlimeColors;
    public GameObject[] MonsterSplatPrefabs;
    //public Texture2D[] BarTextures;
    public UITexture FrontBar;

    private const float BarrelSpawnMinTime = 2;
    private const float BarrelSpawnMaxTime = 5;
    private int Wave;
    private int[] WaveTimers = new int[] { 2, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    private int[] WaveMinEnemies = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 11, 15 };
    private int[] WaveMaxEnemies = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 12, 17 };
    private float WaveTimerForNext;
    private float AutoShootCooldown;
    private float RandomCubeTimer;
    public GameObject RandomCubePrefab;
    public UILabel PointsLabel;
    public int Points;
    public UITexture BulletIcon;
    public GameObject ArenaStage;

    public GameObject LocalPlayerCharacter;
    public List<GameObject> PlayersCharacters = new List<GameObject>();
    public UITexture Go321Sprite;
    public Texture[] Go321Textures;
    private float FillUpTimer;

    // Use this for initialization
    void Start()
    {
        CurrentBullets.Clear();
        CurrentBullets.Add(BulletPrefab[Random.Range(0, BulletPrefab.Length)]);
    }



    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case GameStateId.Initialize:
                {
                    FillUpTimer = 0;
                    AmmoTimer = 1;
                    //				GunPrefab.SetActive(true);
                    
                    NextBarrelSpawnTimer = Random.Range(BarrelSpawnMinTime, BarrelSpawnMaxTime);
                    Wave = 0;
                    WaveTimerForNext = WaveTimers[0];
                    RandomCubeTimer = Random.Range(8, 20);
                    Points = 0;

                    //if(GameController.instance.gameType == GameType.SOLO || PhotonNetwork.isMasterClient)

                    //SpawnAnimin(AniminId.Tbo, AniminEvolutionStageId.Baby);
                    Go321Sprite.gameObject.SetActive(true);
                    Go321Sprite.mainTexture = Go321Textures[0];
                    Go321Sprite.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    TweenScale.Begin(Go321Sprite.gameObject, 0.6f, new Vector3(1.1f, 1.1f, 1.1f));
                    MeterBar.width = 0;

                    if (GameController.instance.gameType == GameType.NETWORK)
                    {
                        State = GameStateId.WaitForPlayersToConnect;
                    }
                    else
                    {
                        State = GameStateId.PrepareToStart3;
                    }

                    GameObject animin = SpawnAniminStart(AniminId.Tbo, AniminEvolutionStageId.Baby);

                    UIGlobalVariablesScript.Singleton.Joystick.CharacterAnimationRef = animin.GetComponent<AnimationControllerScript>();
                    UIGlobalVariablesScript.Singleton.Joystick.CharacterControllerRef = animin.GetComponent<CharacterControllerScript>();


                    break;
                }

     
            case GameStateId.WaitForPlayersToConnect:
                {
                    if (PhotonNetwork.countOfPlayers == 2)
                    {
                        List<GameObject> animinsSpawned = new List<GameObject>();

                        for (int i = 0; i < PhotonNetwork.countOfPlayers; ++i)
                        {
                            GameObject animin = SpawnAniminStart(AniminId.Tbo, AniminEvolutionStageId.Baby);
                            animinsSpawned.Add(animin);
                            //GetComponent<PhotonView>().RPC("ReceiveEventAcquireControlOfCharacter", PhotonNetwork.playerList[i], i);
                        }

                        UIGlobalVariablesScript.Singleton.Joystick.CharacterAnimationRef = animinsSpawned[0].GetComponent<AnimationControllerScript>();
                        UIGlobalVariablesScript.Singleton.Joystick.CharacterControllerRef = animinsSpawned[1].GetComponent<CharacterControllerScript>();

                        SendEventBeginGame();
                    }
                    break;
                }

            case GameStateId.PrepareToStart3:
                {
                    if (Go321Sprite.gameObject.transform.localScale.x == 1.1f)
                    {
                        Go321Sprite.mainTexture = Go321Textures[1];
                        Go321Sprite.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                        TweenScale.Begin(Go321Sprite.gameObject, 0.6f, new Vector3(1.1f, 1.1f, 1.1f));
                        State = GameStateId.PrepareToStart2;
                    }

                    FillUpTimer += Time.deltaTime * 0.40f;
                    MeterBar.width = (int)(1679 * FillUpTimer);
                    MeterBar.uvRect = new Rect(0, 0, FillUpTimer, 1);
                    MeterBar.MarkAsChanged();

                    break;
                }
            case GameStateId.PrepareToStart2:
                {
                    if (Go321Sprite.gameObject.transform.localScale.x == 1.1f)
                    {
                        Go321Sprite.mainTexture = Go321Textures[2];
                        Go321Sprite.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                        TweenScale.Begin(Go321Sprite.gameObject, 0.6f, new Vector3(1.1f, 1.1f, 1.1f));
                        State = GameStateId.PrepareToStart1;
                    }

                    FillUpTimer += Time.deltaTime * 0.40f;
                    MeterBar.width = (int)(1679 * FillUpTimer);
                    MeterBar.uvRect = new Rect(0, 0, FillUpTimer, 1);
                    MeterBar.MarkAsChanged();

                    break;
                }
            case GameStateId.PrepareToStart1:
                {
                    if (Go321Sprite.gameObject.transform.localScale.x == 1.1f)
                    {
                        Go321Sprite.mainTexture = Go321Textures[3];
                        Go321Sprite.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                        TweenScale.Begin(Go321Sprite.gameObject, 0.6f, new Vector3(1.1f, 1.1f, 1.1f));
                        State = GameStateId.PrepareToStartGO;
                    }

                    FillUpTimer += Time.deltaTime * 0.40f;
                    MeterBar.width = (int)(1679 * FillUpTimer);
                    MeterBar.uvRect = new Rect(0, 0, FillUpTimer, 1);
                    MeterBar.MarkAsChanged();

                    break;
                }
            case GameStateId.PrepareToStartGO:
                {
                    if (Go321Sprite.gameObject.transform.localScale.x == 1.1f)
                    {
                        Go321Sprite.gameObject.SetActive(false);
                        State = GameStateId.Countdown;
                    }

                    FillUpTimer += Time.deltaTime * 0.40f;
                    MeterBar.width = (int)(1679 * FillUpTimer);
                    MeterBar.uvRect = new Rect(0, 0, FillUpTimer, 1);
                    MeterBar.MarkAsChanged();

                    break;
                }


            case GameStateId.PrepareToStart:
                {
                    State = GameStateId.Countdown;
                    break;
                }

            case GameStateId.Countdown:
                {
                    State = GameStateId.Playing;

                    UIGlobalVariablesScript.Singleton.SoundEngine.PlayLoop(GenericSoundId.GunLoop);
                    //GameObject.Find("GunfireLoop").GetComponent<AudioSource>().Play();

                    break;
                }

            case GameStateId.Playing:
                {
                    //if(Input.GetButtonDown("Fire1"))
                    //	ShootBulletForward();

                    if (GameController.instance.gameType == GameType.SOLO || PhotonNetwork.isMasterClient)
                    {

                        NextBarrelSpawnTimer -= Time.deltaTime;
                        if (NextBarrelSpawnTimer <= 0)
                        {
                            NextBarrelSpawnTimer = Random.Range(BarrelSpawnMinTime, BarrelSpawnMaxTime);
                            if (Random.Range(0, 10) == 0)
                                SpawnBarrelStart(true);
                            else
                                SpawnBarrelStart(false);
                        }

                        RandomCubeTimer -= Time.deltaTime;
                        if (RandomCubeTimer <= 0)
                        {
                            SpawnRandomCube();
                            RandomCubeTimer = Random.Range(8, 20);
                        }

                        AmmoTimer -= Time.deltaTime * 0.03f;
                        if (AmmoTimer <= 0.001f)
                        {
                            AmmoTimer = 0.001f;
                            State = GameStateId.Completed;
                        }

                      
                        if (GameController.instance.gameType == GameType.NETWORK)
                        {
                            SendEventAmmoTimer();
                        }
                        else
                        {
                            ReceiveEventAmmoTimer(AmmoTimer);
                        }

                        WaveTimerForNext -= Time.deltaTime;
                        if (WaveTimerForNext <= 0)
                        {
                            Wave++;
                            if (Wave == WaveTimers.Length)
                            {
                                State = GameStateId.Completed;
                            }
                            else
                            {
                                WaveTimerForNext = WaveTimers[Wave];
                                int enemyCount = Random.Range(WaveMinEnemies[Wave], WaveMaxEnemies[Wave]);
                                for (int a = 0; a < enemyCount; ++a)
                                {
                                    SpawnEnemyStart(0);
                                }
                            }

                        }

                        AutoShootCooldown -= Time.deltaTime;
                        if (AutoShootCooldown <= 0)
                        {
                            AutoShootCooldown = 0.18f;
                            //UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.Jump);

                            for (int a = 0; a < PlayersCharacters.Count; ++a)
                            {
                                ShootBulletForwardStart(a);
                            }
                        }
                    }

                    //MeterBar.renderer.set = (int)((AmmoTimer) * MeterBarBackground.width);

                    break;
                }

            case GameStateId.Paused:
                {
                    break;
                }

            case GameStateId.Completed:
                {
                    State = GameStateId.PrepareToExit;
                    UIGlobalVariablesScript.Singleton.SoundEngine.StopLoop();
                    //GameObject.Find("GunfireLoop").GetComponent<AudioSource>().Stop();

                    break;
                }

            case GameStateId.PrepareToExit:
                {
                    UIClickButtonMasterScript.HandleClick(UIFunctionalityId.CloseCurrentMinigame, null);

                    if (Points >= 10000)
                        AchievementsScript.Singleton.Show(AchievementTypeId.Gold, Points);
                    else if (Points >= 5000)
                        AchievementsScript.Singleton.Show(AchievementTypeId.Silver, Points);
                    else
                        AchievementsScript.Singleton.Show(AchievementTypeId.Bronze, Points);

                    CharacterProgressScript progressScript = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();
                    progressScript.CurrentAction = ActionId.ExitPortalMainStage;

                    break;
                }
        }

        PointsLabel.text = Points.ToString() + " pts";
    }

    [RPC]
    protected void ReceiveEventAmmoTimer(float ammo)
    {
        MeterBar.width = (int)(1679 * ammo);
        MeterBar.uvRect = new Rect(0, 0, ammo, 1);
        MeterBar.MarkAsChanged();
        //MeterBar.material.mainTextureOffset = new Vector2( AmmoTimer, 0);
    }

    protected void SendEventAmmoTimer()
    {
        GetComponent<PhotonView>().RPC("ReceiveEventAmmoTimer", PhotonTargets.All, AmmoTimer);
    }

    [RPC]
    protected void ReceiveEventAcquireControlOfCharacter(int playerIndex)
    {
        UIGlobalVariablesScript.Singleton.Joystick.CharacterAnimationRef = PlayersCharacters[playerIndex].GetComponent<AnimationControllerScript>();
        UIGlobalVariablesScript.Singleton.Joystick.CharacterControllerRef = PlayersCharacters[playerIndex].GetComponent<CharacterControllerScript>();
    }


    [RPC]
    protected void ReceiveBeginGame()
    {
        State = GameStateId.PrepareToStart3;
    }

    protected void SendEventBeginGame()
    {
        GetComponent<PhotonView>().RPC("ReceiveBeginGame", PhotonTargets.All);
    }

    public void Reset()
    {
        State = GameStateId.Initialize;
    }

    public void CloseGame()
    {
        //GunPrefab.SetActive(false);
        this.gameObject.SetActive(false);

        for (int i = 0; i < SpawnedObjects.Count; ++i)
            Destroy(SpawnedObjects[i]);

        for (int i = 0; i < PlayersCharacters.Count; ++i)
            Destroy(PlayersCharacters[i]);

        PlayersCharacters.Clear();

        SpawnedObjects.Clear();
    }

    public void OnHitByEnemy(GameObject enemy, GameObject character)
    {
        AmmoTimer -= 0.2f;

        //TemporaryDisableCollisionEvent collisionEvent = new TemporaryDisableCollisionEvent(character);
        //PresentationEventManager.Create(collisionEvent);
        character.GetComponent<CharacterControllerScript>().Forces.Add(
            new CharacterForces() { Speed = 800, Direction = -character.transform.forward, Length = 0.3f }
        );


        Debug.Log("OnHitByEnemy");

     /*   int randomCount = Random.Range(5, 8);
        for (int i = 0; i < randomCount; ++i)
        {
            ShootBulletLost(Random.Range(0.30f, 0.60f), character);
        }*/

        UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.GunGame_Bump_Into_Baddy);

        //		GameObject instance = (GameObject)Instantiate(enemy.GetComponent<GunGameEnemyScript>().Splat);
        //		instance.transform.parent = enemy.transform.parent;
        //		instance.transform.position = enemy.transform.position;
        //		instance.transform.rotation = Quaternion.Euler(instance.transform.rotation.eulerAngles.x, instance.transform.rotation.eulerAngles.y, Random.Range(0, 360));
        //		
        //		UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Add(instance);
    }

    public void OnBulletHitBarrel(BarrelCollisionScript barrel)
    {
        MeterBar.mainTexture = barrel.BarFrontTexture;
        BulletIcon.mainTexture = barrel.BuletIcon;

        //Destroy(barrel);
        AmmoTimer += 0.07f;
        if (AmmoTimer >= 1) AmmoTimer = 1;
        string[] prefabs = barrel.BulletPrefabs;
        CurrentBullets.Clear();
        CurrentBullets.AddRange(prefabs);

        Points += 100;

        UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.GunGame_barrel_destroy);

        if (barrel.DestroyedPrefab != null)
        {
            GameObject newProjectile = Instantiate(barrel.DestroyedPrefab) as GameObject;
            newProjectile.transform.parent = barrel.transform.parent;
            newProjectile.transform.position = Vector3.zero;
            newProjectile.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            newProjectile.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            newProjectile.transform.localPosition = barrel.transform.localPosition;
            UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Add(newProjectile);
        }

        UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Remove(this.gameObject);
        Destroy(barrel.gameObject);
    }

    public GameObject SpawnAniminStart(AniminId animinid, AniminEvolutionStageId evolution)
    {
        //string modelPath = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterSwapManagementScript>().GetModelPath(animinid, evolution);
        //RuntimeAnimatorController controller = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterSwapManagementScript>().GetAnimationControlller(animinid, evolution);

        //Object resource1 = Resources.Load("Prefabs/tbo_baby_multi");
        //	Object resource = Resources.Load(modelPath);
        //GameObject childModel = GameObject.Instantiate(resource) as GameObject;

        GameObject instance = null;//GameObject.Instantiate(resource1) as GameObject;

        if (GameController.instance.gameType == GameType.SOLO)
        {
            Object resource1 = Resources.Load("Prefabs/tbo_baby_multi");
            instance = GameObject.Instantiate(resource1) as GameObject;
        }
        else
        {
            instance = PhotonNetwork.Instantiate("Prefabs/tbo_baby_multi", Vector3.zero, Quaternion.identity, 0);
        }

        instance.GetComponent<CharacterControllerScript>().SetLocal(true);
        LocalPlayerCharacter = instance;
        return instance;
    }

    public void SpawnAniminEnd(GameObject instance)
    {
        Vector3 scale = instance.transform.localScale;

        instance.transform.parent = this.transform;
        instance.transform.localPosition = new Vector3(0, 0.1f, 0);
        instance.transform.localScale = scale;
        instance.transform.localRotation = Quaternion.identity;

        for (int i = 0; i < instance.transform.childCount; ++i)
        {
            GameObject childGun = instance.transform.GetChild(i).gameObject;
            if (childGun.name == "gun")
            {
                childGun.SetActive(true);
            }
        }

        PlayersCharacters.Add(instance);
    }

    public GameObject SpawnEnemyStart(int level)
    {
        if (level >= EnemyPrefabs.Length) level = EnemyPrefabs.Length - 1;

        GameObject resourceLoad = Resources.Load(EnemyPrefabs[level]) as GameObject;

        Texture2D[] textures = SlimeTextures;
        if (level == 1) textures = SlimeLevel2Textures;
        int textureIndex = Random.Range(0, textures.Length);

        GameObject newEnemy = null;
        if (GameController.instance.gameType == GameType.SOLO)
        {
            newEnemy = Instantiate(resourceLoad) as GameObject;
        }
        else
        {
            newEnemy = PhotonNetwork.Instantiate(EnemyPrefabs[level], Vector3.zero, Quaternion.identity, 0, new object[] { level, textureIndex });
        }

        newEnemy.GetComponent<GunGameEnemyScript>().SetLocal(true);


        if (GameController.instance.gameType == GameType.SOLO)
        {
            SpawnEnemyEnd(newEnemy, level, textureIndex);
        }


        return newEnemy;
    }

    public void SpawnEnemyEnd(GameObject newEnemy, int level, int textureIndex)
    {
        Texture2D[] textures = SlimeTextures;
        if (level == 1) textures = SlimeLevel2Textures;

        float scale = 1;
        if (level == 1) scale = 3;

        newEnemy.transform.parent = this.transform;
        newEnemy.transform.position = new Vector3(0, 0, 0);
        newEnemy.transform.rotation = Quaternion.identity;
        newEnemy.transform.localScale = new Vector3(0.06f * scale, 0.06f * scale, 0.06f * scale);
        newEnemy.transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));

        //if(level == 1)
        //	newProjectile.transform.rotation = Quaternion.Euler(15, 0, 0);

       
        Texture2D texture = textures[textureIndex];

        for (int i = 0; i < newEnemy.transform.childCount; ++i)
            if (newEnemy.transform.GetChild(i).renderer != null)
                newEnemy.transform.GetChild(i).renderer.material.mainTexture = texture;

        newEnemy.GetComponent<GunGameEnemyScript>().BulletSplat = BulletSplats[textureIndex];
        newEnemy.GetComponent<GunGameEnemyScript>().SkinColor = SlimeColors[textureIndex];
        newEnemy.GetComponent<GunGameEnemyScript>().Speed = Random.Range(0.05f, 0.11f) * (level + 1);
        newEnemy.GetComponent<GunGameEnemyScript>().SplatSetByCode = MonsterSplatPrefabs[textureIndex];
        newEnemy.GetComponent<GunGameEnemyScript>().Level = level;
        newEnemy.GetComponent<GunGameEnemyScript>().TargetToFollow = PlayersCharacters[Random.Range(0, PlayersCharacters.Count)];
        newEnemy.name = texture.name;
        SpawnedObjects.Add(newEnemy);
    }

    public void SpawnBarrelStart(bool special)
    {
        GameObject newProjectile = null;

        if (GameController.instance.gameType == GameType.SOLO)
        {
            if (special)
            {
                GameObject resourceLoad = Resources.Load(SpecialBarrels[Random.Range(0, SpecialBarrels.Length)]) as GameObject;
                newProjectile = Instantiate(resourceLoad) as GameObject;
            }
            else
            {
                GameObject resourceLoad = Resources.Load(Barrels[Random.Range(0, Barrels.Length)]) as GameObject;
                newProjectile = Instantiate(resourceLoad) as GameObject;
            }
        }
        else
        {
            if (special)
            {
                newProjectile = PhotonNetwork.Instantiate(SpecialBarrels[Random.Range(0, SpecialBarrels.Length)], Vector3.zero, Quaternion.identity, 0) as GameObject;
            }
            else
            {
                newProjectile = PhotonNetwork.Instantiate(Barrels[Random.Range(0, Barrels.Length)], Vector3.zero, Quaternion.identity, 0) as GameObject;
            }
        }

        newProjectile.GetComponent<BarrelCollisionScript>().SetLocal(true);

        if (GameController.instance.gameType == GameType.SOLO)
            SpawnBarrelEnd(newProjectile);
    }

    public void SpawnBarrelEnd(GameObject newProjectile)
    {
        newProjectile.transform.parent = this.transform;
        newProjectile.transform.position = Vector3.zero;
        newProjectile.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        newProjectile.transform.localScale = new Vector3(0.19f, 0.19f, 0.19f);
        newProjectile.transform.localPosition = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));

        SpawnedObjects.Add(newProjectile);
    }

    public void SpawnRandomCube()
    {
        GameObject newProjectile = Instantiate(RandomCubePrefab) as GameObject;

        newProjectile.transform.parent = this.transform;
        newProjectile.transform.position = Vector3.zero;
        newProjectile.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        newProjectile.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        newProjectile.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

        SpawnedObjects.Add(newProjectile);
    }

    public void ShootBulletForwardStart(int playerIndex)
    {
        bool isMasterClientShooting = false;

        GameObject newProjectile = null;
        if (GameController.instance.gameType == GameType.SOLO)
        {
            GameObject resourceLoad = Resources.Load(CurrentBullets[Random.Range(0, CurrentBullets.Count)]) as GameObject;
            newProjectile = Instantiate(resourceLoad) as GameObject;
        }
        else
        {
            //if (playerCharacter == LocalPlayerCharacter)
            //    isMasterClientShooting = true;

            newProjectile = PhotonNetwork.Instantiate(
                CurrentBullets[Random.Range(0, CurrentBullets.Count)], 
                Vector3.zero, 
                Quaternion.identity, 
                0,
                new object[] { playerIndex });
        }

        newProjectile.GetComponent<ProjectileScript>().SetLocal(true);

        if (GameController.instance.gameType == GameType.SOLO)
            ShootBulletForwardEnd(newProjectile, PlayersCharacters[playerIndex]);
    }

    public void ShootBulletForwardEnd(GameObject newProjectile, GameObject characterShooting)
    {
        newProjectile.transform.parent = this.transform;
        newProjectile.transform.position = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.position;
        newProjectile.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        newProjectile.transform.localScale = new Vector3(0.116f, 0.116f, 0.116f);
        newProjectile.transform.localPosition = characterShooting.transform.localPosition + characterShooting.transform.forward * 0.14f + new Vector3(0, 0.05f, 0);
        //newProjectile.AddComponent<ProjectileScript>();
        //newProjectile.velocity = transform.TransformDirection( Vector3( 0, 0, speed) );
        //newProjectile.AddComponent<MeshCollider>();
        newProjectile.GetComponent<Rigidbody>().AddForce(characterShooting.transform.forward * 20000);
        SpawnedObjects.Add(newProjectile);
    }



    public void ShootBulletLost(float speedVariationFactor, GameObject character)
    {
        GameObject resourceLoad = Resources.Load(CurrentBullets[Random.Range(0, CurrentBullets.Count)]) as GameObject;

        GameObject newProjectile = Instantiate(resourceLoad) as GameObject;
       // Destroy(newProjectile.GetComponent<ProjectileScript>());
        newProjectile.transform.parent = this.transform;
        newProjectile.transform.position = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.position;
        newProjectile.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        newProjectile.transform.localScale = new Vector3(0.116f, 0.116f, 0.116f) * Random.Range(0.80f, 1.0f);
        newProjectile.transform.localPosition = character.transform.localPosition + character.transform.forward * 0.14f + new Vector3(0, 0.3f, 0);
        //newProjectile.AddComponent<ProjectileScript>();
        //newProjectile.velocity = transform.TransformDirection( Vector3( 0, 0, speed) );
        //newProjectile.AddComponent<MeshCollider>();

        Vector3 direction = Vector3.zero;
        direction.x = Random.Range(-0.3f, 0.3f);
        direction.z = Random.Range(-0.3f, 0.3f);


        newProjectile.GetComponent<Rigidbody>().AddForce((character.transform.up + direction) * 20000 * speedVariationFactor);

        SpawnedObjects.Add(newProjectile);
    }


    public void ShootEnemyDestroyedEffects(float speedVariationFactor, Vector3 position, Color color, string bulletSplat)
    {
        GameObject newProjectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        newProjectile.transform.parent = this.transform;
        newProjectile.transform.position = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.position;
        newProjectile.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        newProjectile.transform.localScale = new Vector3(0.116f, 0.116f, 0.116f) * Random.Range(0.20f, 0.70f);
        newProjectile.transform.localPosition = position;

        Vector3 direction = Vector3.zero;
        direction.x = Random.Range(-0.3f, 0.3f);
        direction.z = Random.Range(-0.3f, 0.3f);

        //newProjectile.layer = LayerMask.NameToLayer("Default");
        //newProjectile.tag = "Untagged";

        newProjectile.layer = LayerMask.NameToLayer("Projectiles");
        newProjectile.AddComponent<Rigidbody>();
        newProjectile.GetComponent<Rigidbody>().velocity = (Vector3.up + direction) * 500 * speedVariationFactor;
        newProjectile.AddComponent<MonsterSplatExplosionScript>();
        newProjectile.GetComponent<MonsterSplatExplosionScript>().SplatPrefab = bulletSplat;

        //newProjectile.GetComponent<Rigidbody>().AddForce(Vector3.down * 20000);
        newProjectile.renderer.material.color = color;
        //newProjectile.GetComponent<Rigidbody>().AddForce((Vector3.up + direction) * 5000 * speedVariationFactor);
        //newProjectile.GetComponent<Rigidbody>().AddExplosionForce(10, position + new Vector3(0,1,0), 1);


        SpawnedObjects.Add(newProjectile);
    }
}
