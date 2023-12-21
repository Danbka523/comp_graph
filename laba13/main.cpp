#include "Headers.h"
#include "Mesh.h"
#include "Shader.h"
#include "Scene.h"
#include <iostream>

using namespace std;


void SetIcon(sf::Window& wnd);

bool isCamActive = false;
bool isCamTouched = false;
glm::vec2 mousePos;
glm::vec2 mouseDelta;


int main() {
	sf::Window window(sf::VideoMode(SCREEN_WIDTH, SCREEN_HEIGHT), "sila SILUSHKA", sf::Style::Default, sf::ContextSettings(24));
	SetIcon(window);
	window.setVerticalSyncEnabled(true);
	window.setActive(true);
	GLenum errorcode = glewInit();
	if (errorcode != GLEW_OK) {
		throw std::runtime_error(std::string(reinterpret_cast<const char*>(glewGetErrorString(errorcode))));
	}
	glEnable(GL_DEPTH_TEST);
	//glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);
	const string obj = "model.obj";
	const string tex = "skull.jpg";
	const string ver = "vertex.vert";
	const string fra = "fragment.frag";
	int count = 10;
	Scene* s = new Scene(obj,tex,ver,fra,count);
	while (window.isOpen()) {
		sf::Event event;
		while (window.pollEvent(event)) {
			if (event.type == sf::Event::Closed) { window.close(); } // poll events
			else if (event.type == sf::Event::Resized) {
				glViewport(0, 0, event.size.width,
					event.size.height);
			}
			else if (event.type == sf::Event::KeyPressed) {
				switch (event.key.code)
				{
				case sf::Keyboard::W:
					s->camera.ProcessKeyboard(Camera::FORWARD, s->GetDeltaTime());
					break;

				case sf::Keyboard::A:
					s->camera.ProcessKeyboard(Camera::LEFT, s->GetDeltaTime());
					break;

				case sf::Keyboard::S:
					s->camera.ProcessKeyboard(Camera::BACKWARD, s->GetDeltaTime());
					break;

				case sf::Keyboard::D:
					s->camera.ProcessKeyboard(Camera::RIGHT, s->GetDeltaTime());
					break;
				case sf::Keyboard::Q:
					s->camera.ProcessKeyboard(Camera::UP, s->GetDeltaTime());
					break;
				case sf::Keyboard::E:
					s->camera.ProcessKeyboard(Camera::DOWN, s->GetDeltaTime());
					break;
				case sf::Keyboard::I:
					s->camera.ProcessRotation(0.1f);
					break;
				case sf::Keyboard::K:
					s->camera.ProcessRotation(-0.1f);
					break;
				case sf::Keyboard::L:
					s->camera.ProcessRotation(0.0f,1.0f);
					break;
				case sf::Keyboard::J:
					s->camera.ProcessRotation(0.0f,-1.0f);
					break;
				default:
					break;
				}
			}
		}
		s->ResetClock();
		
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		s->Draw();
		checkOpenGLerror();
		window.display();
	}

	return 0;
}

void SetIcon(sf::Window& wnd)
{
	sf::Image image;

	// Вместо рисования пикселей, можно загрузить иконку из файла (image.loadFromFile("icon.png"))
	image.create(16, 16);
	for (int i = 0; i < 16; ++i)
		for (int j = 0; j < 16; ++j)
			image.setPixel(i, j, {
				(sf::Uint8)(i * 16), (sf::Uint8)(j * 16), 0 });

	wnd.setIcon(image.getSize().x, image.getSize().y, image.getPixelsPtr());
}

