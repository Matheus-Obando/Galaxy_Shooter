﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	private Animator _animator;
	// Start is called before the first frame update
	void Start()
	{
		_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		// If 'A' key or left arrow is pressed down
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			_animator.SetBool("Turn_Left", true);
			_animator.SetBool("Turn_Right", false);
		}

		// If 'A' key or left arrow is pressed up
		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			_animator.SetBool("Turn_Left", false);
		}

		// If 'D' key or right arrow is pressed down
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			_animator.SetBool("Turn_Right", true);
			_animator.SetBool("Turn_Left", false);
		}

		// If 'D' key or right arrow is pressed up
		if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
		{
			_animator.SetBool("Turn_Right", false);
		}

	}
}
