using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SignController : MonoBehaviour
{
	public GameObject leftSign;
	public GameObject rightSign;
	
	public GameObject leftSignSprite;
	public GameObject rightSignSprite;

	public GameObject leftSelectionSprite;
	public GameObject upSelectionSprite;
	public GameObject rightSelectionSprite;

	private IDictionary<string, int> spriteIndex;

	private Rigidbody rb;

	public struct Choice
	{
		public string left;
		public string up;
		public string right;

		public Choice(string l, string u, string r)
		{
			left = l;
			up = u;
			right = r;
		}
	}

	private IDictionary<string, Choice> choices;

	private string[] keys;

	private Sprite[] sprites;

	private SpriteRenderer leftSignSR;
    private SpriteRenderer rightSignSR;
    private SpriteRenderer upChoiceSR;
    private SpriteRenderer leftChoiceSR;
    private SpriteRenderer rightChoiceSR;

    private Queue lastKeys;
    private int lastKeysMaxCount = 10;

    private Vector3 startPosition;

    public float speed = -100;

    // Start is called before the first frame update
    void Start()
    {
    	// ToDo: Refactor this to load from file
    	choices = new Dictionary<string, Choice>()
    		{
				{"c_cat", new Choice("f_devil", "f_heart_eyes", "f_shocked")},
				{"c_advocado", new Choice("f_sick_mask", "f_constipated_blush", "f_tongue_side")},
				{"c_america", new Choice("f_shades", "f_devil", "c_poop")},
				{"c_baby", new Choice("f_angel", "c_family", "f_sick_mask")},
				{"c_bacon", new Choice("f_crying", "f_smile", "f_disgusted")},
				{"c_banana", new Choice("c_eggplant", "f_tongue_out_close", "f_no_mouth")},
				{"c_bath", new Choice("c_surfing", "f_wink", "f_asleep")},
				{"c_bear", new Choice("f_side_eye_smile", "f_shocked_hands", "c_banana")},
				{"c_beer", new Choice("f_heart_eyes", "c_police", "f_sad")},
				{"c_bikini", new Choice("f_tongue_out_wink", "f_o_face", "f_disgusted")},
				{"c_birthday_cake", new Choice("f_upside_down", "f_shocked", "f_kiss")},
				{"c_building_broken", new Choice("c_america", "f_crying", "f_worried")},
				{"c_burger", new Choice("f_blank", "f_content", "c_horse")},
				{"c_cancer", new Choice("c_island", "f_overbite", "f_smoke_nose")},
				{"c_cheese", new Choice("f_sparkle_eye_cry", "f_smile", "f_grimace")},
				{"c_christmas_tree", new Choice("f_frown_simple", "f_blush", "f_smile+intense")},
				{"c_coffin", new Choice("c_cancer", "c_smoking", "c_rocket")},
				{"c_cookie", new Choice("c_radiation", "f_overbite_shocked", "f_smile_simple")},
				{"c_crown", new Choice("f_angry_red", "f_side_eyes_frown", "f_tongue_out")},
				{"c_cupid_heart", new Choice("f_angel", "f_shout", "f_shock_sweat")},
				{"c_desert", new Choice("f_nose_bubble", "f_devil", "c_ice_cream")},
				{"c_donut", new Choice("f_heart_eyes", "c_police", "f_overbite_shocked")},
				{"c_eggplant", new Choice("f_heart_eyes", "c_horse", "f_shocked")},
				{"c_family", new Choice("f_sick_mask", "c_prayer", "f_heart_eyes")},
				{"c_gay_kiss", new Choice("c_prayer", "f_kiss_heart", "c_ice_cream")},
				{"c_goat", new Choice("f_grimace", "f_content", "c_bacon")},
				{"c_green_tea", new Choice("f_smile_simple", "f_disgusted", "f_blank")},
				{"c_high_heel", new Choice("c_lesbian_kiss", "f_o_face", "f_content")},
				{"c_horse", new Choice("f_worried", "f_crying", "f_wink")},
				{"c_hospital", new Choice("f_sick_mask", "f_worried_sweat", "f_angel")},
				{"c_ice_cream", new Choice("f_tongue_side", "c_rainbow", "f_angry")},
				{"c_island", new Choice("f_blank_closed_eyes", "f_kiss_heart", "f_smile_sweat")},
				{"c_jack_o_lantern", new Choice("c_police", "f_blank_eyes", "f_shout")},
				{"c_king", new Choice("c_queen", "f_kiss", "c_leo")},
				{"c_leo", new Choice("f_dead_eyes", "c_wolf", "f_smile_sweat")},
				{"c_lesbian_kiss", new Choice("c_lips", "f_wide_smile", "c_rocket")},
				{"c_lips", new Choice("f_kiss_heart", "f_upside_down", "c_taco")},
				{"c_middle_finger", new Choice("f_shocked_hands", "c_police", "f_content")},
				{"c_monkey", new Choice("f_asleep", "f_smile_teeth", "f_worried")},
				{"c_no_smoking", new Choice("f_shades", "f_angry_red", "f_blank")},
				{"c_pancakes", new Choice("f_smile_wide", "f_no_mouth", "f_frown_simple")},
				{"c_peace", new Choice("c_surfing", "c_king", "c_rocket")},
				{"c_peace_sign", new Choice("c_middle_finger", "f_smoke_nose", "f_o_face_eyebrows")},
				{"c_piggie", new Choice("c_bacon", "f_kiss_eyes", "c_police")},
				{"c_pizza", new Choice("f_worried", "f_heart_eyes", "c_beer")},
				{"c_police", new Choice("c_queen", "c_piggie", "c_king")},
				{"c_poop", new Choice("c_ice_cream", "f_disgusted", "c_taco")},
				{"c_prayer", new Choice("f_devil", "f_upside_down", "f_angel")},
				{"c_present", new Choice("f_blush", "c_birthday_cake", "f_tongue_out")},
				{"c_queen", new Choice("f_heart_eyes", "f_shades", "c_bath")},
				{"c_rabbit", new Choice("c_wolf", "f_content", "f_blank_eyes")},
				{"c_radiation", new Choice("f_shocked_hands", "c_peace", "f_sick_mask")},
				{"c_rainbow", new Choice("c_police", "c_gay_kiss", "c_high_heel")},
				{"c_recycle", new Choice("c_volcano", "c_sun_ocean", "f_smile_simple")},
				{"c_rocket", new Choice("f_dead_eyes", "f_shades", "c_banana")},
				{"c_rooster", new Choice("c_burger", "f_shocked_blush", "c_rocket")},
				{"c_rowing", new Choice("f_asleep", "f_smoke_nose", "c_taxi")},
				{"c_saki", new Choice("c_wine", "f_blush", "c_green_tea")},
				{"c_salad", new Choice("f_tounge_side", "c_rabbit", "c_burger")},
				{"c_shrimp", new Choice("c_sun_ocean", "f_disgusted", "f_heart_eyes")},
				{"c_smoking", new Choice("f_no_mouth", "f_smile", "f_dead_eyes")},
				{"c_sun_mountain", new Choice("c_goat", "c_king", "f_smoke_nose")},
				{"c_sun_ocean", new Choice("f_smile_eyes", "c_island", "c_shrimp")},
				{"c_surfing", new Choice("f_asleep", "f_wink", "c_bikini")},
				{"c_sushi", new Choice("c_green_tea", "f_shades", "f_disgusted")},
				{"c_swords", new Choice("c_hospital", "f_angry_red", "f_shock_sweat")},
				{"c_taco", new Choice("c_poop", "f_kiss_blush", "c_burger")},
				{"c_taxi", new Choice("f_asleep", "f_worried", "c_rowing")},
				{"c_tiger", new Choice("c_cat", "f_shades", "c_leo")},
				{"c_tongue", new Choice("c_burger", "c_pancakes", "c_pizza")},
				{"c_trophy", new Choice("c_horse", "f_overbite_sad", "f_smile_teeth")},
				{"c_volcano", new Choice("f_shocked", "c_recycle", "c_smoking")},
				{"c_wine", new Choice("c_lips", "c_bath", "f_no_mouth")},
				{"c_wolf", new Choice("c_rabbit", "f_heart_eyes", "f_overbite")},
				{"c_ying_yang", new Choice("f_angel", "c_cat", "f_devil")}
    		};

    	keys = choices.Keys.ToArray();

    	spriteIndex = new Dictionary<string, int>();
    	sprites = Resources.LoadAll<Sprite>("emoji");

    	for (int i = 0; i < sprites.Length; i++) {
    		spriteIndex.Add(sprites[i].name, i);
    	}

    	leftSignSR = leftSignSprite.GetComponent<SpriteRenderer>();
    	rightSignSR = rightSignSprite.GetComponent<SpriteRenderer>();

    	upChoiceSR = leftSelectionSprite.GetComponent<SpriteRenderer>();
    	leftChoiceSR = upSelectionSprite.GetComponent<SpriteRenderer>();
    	rightChoiceSR = rightSelectionSprite.GetComponent<SpriteRenderer>();

    	lastKeys = new Queue();

    	this.randomize();

    	startPosition = transform.position;
    }

    public void startMoving()
    {
    	rb = GetComponent<Rigidbody>();
    	rb.velocity = new Vector3(0, 0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void next()
    {
    	this.flip();

    	this.randomize();
    	Debug.Log("here");
    	transform.position = startPosition;
    }

    private void randomize()
    {
    	System.Random rand = new System.Random();
    	string choiceSprite;
    	
    	do
    	{
    		choiceSprite = keys[rand.Next(0, keys.Length)];
    	} while (lastKeys.Contains(choiceSprite) == true);

    	lastKeys.Enqueue(choiceSprite);
    	if (lastKeys.Count > lastKeysMaxCount) {
    		lastKeys.Dequeue();
    	}

    	leftSignSR.sprite = sprites[spriteIndex[choiceSprite]];
    	rightSignSR.sprite = sprites[spriteIndex[choiceSprite]];
    	upChoiceSR.sprite = sprites[spriteIndex[choices[choiceSprite].left]];
    	leftChoiceSR.sprite = sprites[spriteIndex[choices[choiceSprite].up]];
    	rightChoiceSR.sprite = sprites[spriteIndex[choices[choiceSprite].right]];


    }

    private void flip()
    {
    	if (leftSign.activeSelf) {
    		this.setRight();
    	} else {
    		this.setLeft();
    	}
    }

    private void setLeft()
    {
    	rightSign.SetActive(false);
    	leftSign.SetActive(true);
    }

    private void setRight()
    {
    	leftSign.SetActive(false);
    	rightSign.SetActive(true);
    }

}
