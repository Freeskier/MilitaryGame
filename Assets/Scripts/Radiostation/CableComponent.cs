
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CableComponent : MonoBehaviour
{
	[SerializeField] private Transform endPoint;
	[SerializeField] private Material cableMaterial;

	[SerializeField] private float cableLength = 0.5f;
	[SerializeField] private int totalSegments = 5;
	[SerializeField] private float segmentsPerUnit = 2f;
	private int segments = 0;
	[SerializeField] private float cableWidth = 0.1f;

	[SerializeField] private int verletIterations = 1;
	[SerializeField] private int solverIterations = 1;

	[SerializeField] private float stiffness = 1f;

	[HideInInspector]
	public LineRenderer line;
	private List<CableParticle> points;
	public GameObject particlePrefab;
	private List<GameObject> particles;



	void Awake()
	{
		InitCableParticles();
	}

	void InitCableParticles()
	{
		if (totalSegments > 0)
			segments = totalSegments;
		else
			segments = Mathf.CeilToInt (cableLength * segmentsPerUnit);

		Vector3 cableDirection = (endPoint.position - transform.position).normalized;
		float initialSegmentLength = cableLength / segments;
		points = new List<CableParticle>();
		particles = new List<GameObject>();

		for (int pointIdx = 0; pointIdx <= segments; pointIdx++) {
			Vector3 initialPosition = transform.position + (cableDirection * (initialSegmentLength * pointIdx));
			points.Add(new CableParticle(initialPosition));
			var g = Instantiate(particlePrefab, initialPosition, Quaternion.identity);
			g.transform.SetParent(transform);
			particles.Add(g);
		}

		CableParticle start = points[0];
		CableParticle end = points[segments];
		start.Bind(this.transform);
		end.Bind(endPoint.transform);
	}


	void Update()
	{
		RenderCable();
	}

	void RenderCable()
	{
		for (int pointIdx = 0; pointIdx < segments + 1; pointIdx++) 
		{
			particles[pointIdx].transform.position = points [pointIdx].Position;

		}
	}



	void FixedUpdate()
	{
		for (int verletIdx = 0; verletIdx < verletIterations; verletIdx++) 
		{
			VerletIntegrate();
			SolveConstraints();
			ApplyRotation();
		}
	}

	void ApplyRotation()
	{
		for (int i = 0; i < segments; i++)
		{
			var lookAtNormalized = (particles[i + 1].transform.position - particles[i].transform.position).normalized;
			var vec = Vector3.Cross(lookAtNormalized, Vector3.right);
			particles[i].transform.rotation = Quaternion.LookRotation(vec);
		}
		particles[segments].transform.rotation = particles[segments-1].transform.rotation;
	}


	void VerletIntegrate()
	{
		Vector3 gravityDisplacement = Time.fixedDeltaTime * Time.fixedDeltaTime * Physics.gravity;
		foreach (CableParticle particle in points) 
		{
			particle.UpdateVerlet(gravityDisplacement);
		}
	}


	void SolveConstraints()
	{
		for (int iterationIdx = 0; iterationIdx < solverIterations; iterationIdx++) 
		{
			SolveDistanceConstraint();
			SolveStiffnessConstraint();
		}
	}



	void SolveDistanceConstraint()
	{
		float segmentLength = cableLength / segments;
		for (int SegIdx = 0; SegIdx < segments; SegIdx++) 
		{
			CableParticle particleA = points[SegIdx];
			CableParticle particleB = points[SegIdx + 1];

			SolveDistanceConstraint(particleA, particleB, segmentLength);
		}
	}
		

	void SolveDistanceConstraint(CableParticle particleA, CableParticle particleB, float segmentLength)
	{
		Vector3 delta = particleB.Position - particleA.Position;
		float currentDistance = delta.magnitude;
		float errorFactor = (currentDistance - segmentLength) / currentDistance;

		if (particleA.IsFree() && particleB.IsFree()) 
		{
			particleA.Position += errorFactor * 0.5f * delta;
			particleB.Position -= errorFactor * 0.5f * delta;
		} 
		else if (particleA.IsFree()) 
		{
			particleA.Position += errorFactor * delta;
		} 
		else if (particleB.IsFree()) 
		{
			particleB.Position -= errorFactor * delta;
		}
	}

	void SolveStiffnessConstraint()
	{
		float distance = (points[0].Position - points[segments].Position).magnitude;
		if (distance > cableLength) 
		{
			foreach (CableParticle particle in points) 
			{
				SolveStiffnessConstraint(particle, distance);
			}
		}	
	}


	void SolveStiffnessConstraint(CableParticle cableParticle, float distance)
	{
	

	}
}