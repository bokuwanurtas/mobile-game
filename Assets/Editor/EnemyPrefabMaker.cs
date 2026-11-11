using UnityEngine;
using UnityEditor;

// Tools -> Enemies -> Create Three Enemy Prefabs
// Собирает Patroller / Shooter / Charger из твоих спрайтов в Assets/Prefabs
public static class EnemyPrefabMaker
{
    [MenuItem("Tools/Enemies/Create Three Enemy Prefabs")]
    static void Create()
    {
        AudioClip hit = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sounds/hit.mp3");
        AudioClip shoot = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sounds/shoot.mp3");
        AudioClip windup = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sounds/pickup.mp3");
        GameObject bullet = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/BossBullets.prefab");

        // Patroller: камень
        GameObject p = Make("Patroller", "Assets/Sprites/rock.png", 1.0f, 1, hit, windup);
        PatrollerEnemy pat = p.AddComponent<PatrollerEnemy>();
        pat.windupClip = windup;
        Save(p);

        // Shooter: монстр босса
        GameObject s = Make("Shooter", "Assets/Sprites/boss.png", 1.4f, 2, hit, windup);
        ShooterEnemy sh = s.AddComponent<ShooterEnemy>();
        sh.windupClip = windup;
        sh.shootClip = shoot;
        sh.projectilePrefab = bullet;
        sh.artFacesRight = false;
        sh.aimLine = MakeAimLine(s.transform);
        Save(s);

        // Charger: бомба
        GameObject c = Make("Charger", "Assets/Sprites/bomb.png", 1.0f, 3, hit, windup);
        ChargerEnemy ch = c.AddComponent<ChargerEnemy>();
        ch.windupClip = windup;
        Save(c);

        AssetDatabase.SaveAssets();
        Debug.Log("Enemy prefabs created in Assets/Prefabs");
    }

    static GameObject Make(string name, string spritePath, float height, int hp, AudioClip hit, AudioClip windup)
    {
        GameObject go = new GameObject(name);

        // первый спрайт из файла (работает и для Single, и для Multiple)
        Sprite sprite = null;
        foreach (Object o in AssetDatabase.LoadAllAssetsAtPath(spritePath))
        {
            if (o is Sprite) { sprite = (Sprite)o; break; }
        }

        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingOrder = 1;

        float scale = height / sprite.bounds.size.y;
        go.transform.localScale = new Vector3(scale, scale, 1);

        Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
        rb.gravityScale = 3;
        rb.freezeRotation = true;
        rb.mass = 10;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        BoxCollider2D box = go.AddComponent<BoxCollider2D>();
        box.size = sprite.bounds.size;
        box.offset = sprite.bounds.center;

        AudioSource src = go.AddComponent<AudioSource>();
        src.playOnAwake = false;

        EnemyHealth eh = go.AddComponent<EnemyHealth>();
        eh.hp = hp;
        eh.hitClip = hit;

        return go;
    }

    static LineRenderer MakeAimLine(Transform parent)
    {
        string matPath = "Assets/Prefabs/AimLine.mat";
        Material mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
        if (mat == null)
        {
            mat = new Material(Shader.Find("Sprites/Default"));
            mat.color = Color.red;
            AssetDatabase.CreateAsset(mat, matPath);
        }

        GameObject child = new GameObject("AimLine");
        child.transform.SetParent(parent, false);

        LineRenderer lr = child.AddComponent<LineRenderer>();
        lr.material = mat;
        lr.positionCount = 2;
        lr.startWidth = 0.06f;
        lr.endWidth = 0.06f;
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.sortingOrder = 5;
        lr.useWorldSpace = true;
        lr.enabled = false;
        return lr;
    }

    static void Save(GameObject go)
    {
        PrefabUtility.SaveAsPrefabAsset(go, "Assets/Prefabs/" + go.name + ".prefab");
        Object.DestroyImmediate(go);
    }
}
