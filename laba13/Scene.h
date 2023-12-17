#pragma once
#include "Headers.h"
#include "Camera.h"
#include "Mesh.h"

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
	Mesh mesh;
	Shader shaders;
	Camera camera;
	Scene(const string& meshPath, const string& tex, const string& vShaderPath,const string& fShaderPath) {
		
		mesh = Mesh(meshPath,tex);
		
		shaders = Shader(vShaderPath, fShaderPath);
		
		init();
		
	}

	~Scene() {
		mesh.ReleaseVBO();
		shaders.Release();
	}
	void init() {
		sf::Clock clock;
		int r = 3000;
		
		positions.push_back(glm::vec3(0, 0, 0));
		for (int i = 1; i < 5; i++) {
			float x = static_cast<float>(rand()) / r;
			float y = static_cast<float>(rand()) / r / 10;
			float z = static_cast<float>(rand()) / r;
			positions.push_back(glm::vec3(x, y, z));
		}

		
		for (int i = 0; i < 5; i++) {
			orbitSpeeds.push_back(static_cast<float>(rand()) / 10000);
		}

		
		sizes.push_back(1.5f);
		for (int i = 1; i < 5; i++) {
			sizes.push_back(1.0f);
		}

		
		modelMatrices.push_back(glm::rotate(glm::mat4(1.0f), glm::radians(clock.getElapsedTime().asSeconds() * 60.0f), glm::vec3(0.0f, 1.0f, 0.0f)));
	}
	void Draw() {
		shaders.use();
		sf::Clock clock;
		camera.UpdateUniforms(&shaders);
		

		for (int i = 1; i < 5; ++i) {
			float orbitRadius = sqrt(positions[i].x * positions[i].x + positions[i].z * positions[i].z);
			float time = clock.getElapsedTime().asSeconds();
			float satelliteX = orbitRadius * cos(orbitSpeeds[i] * time);
			float satelliteZ = orbitRadius * sin(orbitSpeeds[i] * time);

			glm::mat4 model = glm::mat4(1.0f);
			model = glm::translate(model, glm::vec3(satelliteX, 0, satelliteZ));
			model = glm::translate(model, glm::vec3(0, positions[i].y, 0));
			model = glm::rotate(model, glm::radians(clock.getElapsedTime().asSeconds() * 60.0f), glm::vec3(0.0f, 1.0f, 0.0f));
			modelMatrices.push_back(model);
		}
		shaders.use();
		glUniformMatrix4fv(glGetUniformLocation(shaders.ID,"models"), modelMatrices.size(), GL_FALSE, glm::value_ptr(modelMatrices[0]));
		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, mesh.texture);
		glUniform1i(glGetUniformLocation(shaders.ID, "tex"), 0);
		mesh.Draw();

	}

};