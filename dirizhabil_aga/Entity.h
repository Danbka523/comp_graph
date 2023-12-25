#pragma once
#include "Headers.h"
#include "Mesh.h"
#include "Shader.h"
#include "Camera.h"

class Entity
{
	float x = 0, y = 0, z = 0;
	float rx = 0, ry = 0, rz = 0;
	float cx = 1, cy = 1, cz = 1;
	

	void update_uniforms() {
		auto model_matrix = glm::translate(glm::mat4(1.f), position)
			* glm::rotate(glm::mat4(1.f), glm::radians(ry), glm::vec3(0.f, 1.f, 0.f))
			* glm::scale(glm::mat4(1.f), scale);

		shader->SetMat4("model", model_matrix);
	
	}
public:
	string name;
	Mesh* mesh;
	Shader* shader;


	glm::vec3 position{ x,y,z };
	glm::vec3 rotation{ rx,ry,rz };
	glm::vec3 scale{ cx,cy,cz };


	Entity() {}
	Entity(Mesh* mesh, Shader* shader, string name="") {
		this->mesh = mesh;
		this->shader = shader;
		this->name = name;
	}

	~Entity() {
	}


	void draw() {
		if (!mesh || !shader)
			return;
		shader->use();
		update_uniforms();
		glUseProgram(0);
		mesh->Draw(shader);
		checkOpenGLerror();
		glUseProgram(0);
	}

	void moving(float x, float y, float z) {

		position = { position.x+x,position.y+y,position.z+z };
	}

	void rotating(float rx, float ry, float rz) {
		rotation = { rotation.x+rx,rotation.y+ry,rotation.z+rz };
	}

	void scaling(float cx, float cy, float cz) {
		scale = { scale.x+cx,scale.y+cy,scale.z+cz };
	}
};