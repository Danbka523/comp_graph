#pragma once
#include "Headers.h"
#include "Camera.h"
#include "Mesh.h"
#include "Light.h"
#include "Entity.h"
#include<vector>

class Scene {
	vector<glm::vec3> positions;
	vector<glm::mat4> modelMatrices;
	vector<float> sizes;
	vector<float> orbitSpeeds;
protected:

	float deltaTime;
	sf::Clock cl;
	sf::Clock globalCl;

public:
	inline const float GetDeltaTime() { return deltaTime; }

	inline void ResetClock() {
		deltaTime = cl.getElapsedTime().asSeconds();
		cl.restart();
	}
	vector<Mesh> meshes;
	vector<Shader> shaders;
	vector<Entity> entities;
	Camera camera;

	PointLight pl;
	DirLight dl;
	SpotLight sl;
	
	Scene(vector<string>& meshes_path, vector<string>& textures_path, vector<string>& verts_shaders_path, vector<string>& frag_shaders_path) {
		camera = Camera();
		for (size_t i = 0; i < meshes_path.size(); i++)
		{
			meshes.push_back(Mesh(meshes_path[i], textures_path[i]));
			shaders.push_back(Shader(verts_shaders_path[i], frag_shaders_path[i]));
			/*if (i == 0)
				entities.push_back(Entity(&meshes[i], &shaders[i],"grass"));
			else if (i==1)
				entities.push_back(Entity(&meshes[i], &shaders[i], "airship"));
			else if (i==2)		
				entities.push_back(Entity(&meshes[i], &shaders[i], "fir"));
			else if (i==3)
				entities.push_back(Entity(&meshes[i], &shaders[i], "gift"));
			else 
				entities.push_back(Entity(&meshes[i], &shaders[i], "house"));*/
		}
		
		init();
		
	}

	~Scene() {
		for ( auto mesh : meshes)
		{
			mesh.ReleaseVBO();
		}
		for (auto shader : shaders)
		{
			shader.Release();
		}
	}
	void init() {
		init_lights();


		/*
		//int r = 5000;
		//
		//positions.push_back(glm::vec3(0, 0, 0));
		//for (int i = 1; i < 5; i++) {
		//	float x = static_cast<float>(rand()) / r;
		//	float y = static_cast<float>(rand()) / r / 10;
		//	float z = static_cast<float>(rand()) / r;
		//	positions.push_back(glm::vec3(x, y, z));
		//}

		//
		//for (int i = 0; i < 5; i++) {
		//	orbitSpeeds.push_back(static_cast<float>(rand()) / 5000);
		//}

		//
		//sizes.push_back(0.5f);
		//for (int i = 1; i < 5; i++) {
		//	sizes.push_back(0.1f);
		//}
		*/
		

	}

	void move_airship(float x, float y, float z) {
		entities[1].moving(x, y, z);
	}

	void init_grass() {
	
	
	}

	void init_houses() {
		for (size_t i = 4; i <  entities.size(); i++)
		{

		}
	
	}

	void init_lights() {
		for  ( auto& shader : shaders)
		{
			init_light(&shader, &camera);
			dl.SetUniforms(&shader);
		}

	}
	glm::vec3 cubePositions[10] = {
		glm::vec3(0.0f,  -1.f,  0.0f),
		glm::vec3(4.0f,  5.0f, -15.0f),
		glm::vec3(-5.f, -10.f, -2.5f),
		glm::vec3(-3.0f, -2.0f, -12.0f),
		glm::vec3(2.4f, -4.f, -3.5f),
		glm::vec3(-3.f,  3.0f, -7.5f),
		glm::vec3(6.f, -2.0f, -2.5f),
		glm::vec3(7.f,  2.0f, -2.5f),
		glm::vec3(1.5f,  -2.f, -1.5f),
		glm::vec3(-4.f,  1.0f, -1.5f)
	};

	void Draw() {
		/*
		sl.SetUniforms(&shaders);
		dl.SetUniforms(&shaders);
		pl.SetUniforms(&shaders);
		*/
		for (auto& shader : shaders)
		{
			camera.UpdateUniforms(&shader);
		}
		

		for (unsigned int i = 0; i < entities.size(); i++)
		{
			entities[i].shader->use();

			glActiveTexture(GL_TEXTURE0);
		    glBindTexture(GL_TEXTURE_2D, entities[i].mesh->texture);
			auto t = glGetUniformLocation(entities[i].shader->ID, "ourTexture");
			glUniform1i(glGetUniformLocation(entities[i].shader->ID, "ourTexture"), 0);
			entities[i].draw();


			glUseProgram(0);

			/*
			//shaders.use();
			//glm::mat4 model = glm::mat4(1.0f);
			//model = glm::translate(model, cubePositions[i]);
			//float angle = 20.0f * i;
			////model = glm::rotate(model, glm::radians(angle), glm::vec3(1.0f, 0.3f, 0.5f));
			//shaders.SetMat4("model", model);
			//checkOpenGLerror();
			//glActiveTexture(GL_TEXTURE0);
			//glBindTexture(GL_TEXTURE_2D, mesh.texture);
			//glUniform1i(glGetUniformLocation(shaders.ID, "tex"), 0);
			//mesh.Draw();
			//glUseProgram(0);
			*/
		}
	}

};