#pragma once
#include "Headers.h"
#include "Mesh.h"
#include "Shader.h"
#include "Camera.h"

class Entity
{
	float x=0, y=0, z=0;
	float rx=0, ry=0, rz=0;
	float cx = 1, cy = 1, cz = 1;

	void update_uniforms() {
		auto model_matrix = glm::translate(glm::mat4(1.f), position)
			* glm::rotate(glm::mat4(1.f), rotation.y, glm::vec3(0.f, 1.f, 0.f))
			* glm::scale(glm::mat4(1.f), scale);

		shader->SetMat4("model", model_matrix);
	
	}
public:
	string name;
	Mesh* mesh = nullptr;
	Shader* shader = nullptr;


	glm::vec3 position{ x,y,z };
	glm::vec3 rotation{ rx,ry,rz };
	glm::vec3 scale{ cx,cy,cz };


	Entity() {}
	Entity(Mesh* mesh, Shader* shader, string name="") {
		this->mesh = mesh;
		this->shader = shader;
		this->name = name;
	}

	~Entity() {}


	void draw() {
		if (!mesh || !shader)
			return;

		shader->use();
		update_uniforms();
		mesh->Draw();
		glUseProgram(0);
	}

	void move(float x, float y, float z) {
		this->x += x;
		this->y += y;
		this->z += z;
		position = { x,y,z };
	}

	void rotate(float rx, float ry, float rz) {
		this->rx += rx;
		this->ry += ry;
		this->rz += rz;
		rotation = { rx,ry,rz };
	}

	void scale(float cx, float cy, float cz) {
		this->cx += cx;
		this->cy += cy;
		this->cz += cz;
		scale = { cx,cy,cz };
	}
};