#pragma once
#include "Headers.h"
#include "Shader.h"
void init_light(const Shader* s, const Camera* c);
const Camera* camera;


 void init_light(const Shader* s,const Camera* c) {
	s->use();
	float shininess = 32.0f;
	s->SetFloat("material.shininess", shininess);
	glm::vec4 diffuse{ 0.7f, 0.7f, 0.7f, 1.f };
	s->SetVec3("materila.diffuse", diffuse);
	glUseProgram(0);
	camera = c;
}


struct DirLight {
	glm::vec3 direction{ -0.2f, -1.0f, -0.3f };
	glm::vec3 ambient{ 0.2f, 0.2f, 0.2f };
	glm::vec3 diffuse{ 0.5f, 0.5f, 0.5f};
	glm::vec3 specular{ 1.0f, 1.0f, 1.0f};

	void SetUniforms(const Shader* s) {
		//glm::vec3 direction = camera->Front;
		glm::vec3 position = camera->Position;
		s->use();
		s->SetVec3("viewPos", position);

		s->SetVec3("dirLight.direction", direction);
		s->SetVec3("dirLight.ambient", ambient);
		s->SetVec3("dirLight.diffuse", diffuse);
		s->SetVec3("dirLight.specular", specular);

		glUseProgram(0);
	}
};

struct PointLight {
	glm::vec3 lposition{ 2.0f,0.0f,0.0f };
	glm::vec3 ambient{ 0.2f, 0.2f, 0.2f, };
	glm::vec3 diffuse{ 1.0f, 1.0f, 1.0f };
	glm::vec3 specular{ 1.0f, 1.0f, 1.0f };
	float constant = 1.0f;
	float linear = 0.09f;
	float quadratic = 0.032f;

	void SetUniforms(const Shader* s) {


		glm::vec3 direction = camera->Front;
		glm::vec3 position = camera->Position;
		s->use();
		
		s->SetVec3("viewPos", position);

		s->SetVec3("pointLight.position", lposition);
		s->SetVec3("pointLight.ambient", ambient);
		s->SetVec3("pointLight.diffuse", diffuse);
		s->SetVec3("pointLight.specular", specular);
		s->SetFloat("pointLight.constant", constant);
		s->SetFloat("pointLight.linear", linear);
		s->SetFloat("pointLight.quadratic", quadratic);

		glUseProgram(0);
	}

};

struct SpotLight {

	glm::vec3 ambient{ 0.2f, 0.2f, 0.2f, };
	//glm::vec3 lposition{-1.f, 0.0f, 0.0f, };
	glm::vec3 direction{ -1.0f,0.0f, 0.0f, };
	glm::vec3 diffuse{ 1.0f, 1.0f, 1.0f };
	glm::vec3 specular{ 1.0f, 1.0f, 1.0f };
	float constant = 1.0f;
	float linear = 0.09f;
	float quadratic = 0.032f;
	
	float cutOff = glm::cos(glm::radians(12.5));
	float outerCutOff = glm::cos(glm::radians(12.5));
	void SetUniforms(const Shader* s) {

		glm::vec3 direction = camera->Front;
		glm::vec3 position = camera->Position;

		s->use();
		s->SetVec3("viewPos", position);
		s->SetVec3("spotLight.position", position);
		s->SetVec3("spotLight.direction", direction);
		s->SetVec3("spotLight.ambient", ambient);
		s->SetVec3("spotLight.diffuse", diffuse);
		s->SetVec3("spotLight.specular", specular);
		s->SetFloat("spotLight.constant", constant);
		s->SetFloat("spotLight.linear", linear);
		s->SetFloat("spotLight.quadratic", quadratic);
		s->SetFloat("spotLight.cutOff", cutOff);
		s->SetFloat("spotLight.outerCutOff", outerCutOff);

		glUseProgram(0);
	}
};