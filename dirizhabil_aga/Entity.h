#pragma once
#include "Headers.h"
#include "Mesh.h"
#include "Shader.h"
#include "Camera.h"

class Entity
{
	float x, y, z;
	float rx=1, ry=0, rz=0;
	float cx = 1, cy = 1, cz = 1;
	float degrees=0;

	void update_uniforms() {
		auto model_matrix = glm::translate(glm::mat4(1.f), position)
			* glm::rotate(glm::mat4(1.f), glm::radians(degrees), rotation)
			* glm::scale(glm::mat4(1.f), scale);

		shader->SetMat4("model", model_matrix);
	
	}
public:
	string name;
	Mesh* mesh;
	Shader* shader;
	float radius;

	glm::vec3 position{ x,y,z };
	glm::vec3 rotation{ rx,ry,rz };
	glm::vec3 scale{ cx,cy,cz };


	Entity() {}
	Entity(Mesh* mesh, Shader* shader, string name="", float radius=0) {
		this->mesh = mesh;
		this->shader = shader;
		this->name = name;
		this->radius = radius;
	}

	~Entity() {
	}

	void set_pos(glm::vec3 pos) {
		x = pos.x;
		y = pos.y;
		z = pos.z;
		position = glm::vec3(this->x, this->y, this->z);
	}

	void draw() {
		if (!mesh || !shader)
			return;
		shader->use();
		update_uniforms();
		mesh->Draw(shader);
		checkOpenGLerror();
		glUseProgram(0);
	}

	void moving(float x, float y, float z) {
		this->x += x;
		this->y += y;
		this->z += z;
		position = glm::vec3( this->x,this->y,this->z );
	}

	void rotating(float degrees,glm::vec3 rot) {
		rotation = rot;
		this->degrees = degrees;
	}

	void scaling(float cx, float cy, float cz) {
		
		x *= cx;
		y *= cy;
		z *= cz;
		position = glm::vec3(x, y, z);
		this->cx = cx;
		this->cy = cy;
		this->cz = cz;
		scale = { cx,cy,cz };
	}
};