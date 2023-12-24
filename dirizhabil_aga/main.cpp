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

static void make_scene(Scene* s) {

	auto grass = Entity(&s->meshes[0], &s->shaders[0]);
	s->entities.push_back(grass);
	grass.position += glm::vec3(0,0,-2.0f);
	grass.scale+=glm::vec3(2, 2, 2);

	auto airship = Entity(&s->meshes[1], &s->shaders[1]);
	airship.position += glm::vec3(2.5f, 0.f, 0.0f);
	s->entities.push_back(airship);

}

int main() {
	sf::Window window(sf::VideoMode(SCREEN_WIDTH, SCREEN_HEIGHT), "dirizhabyl' aga", sf::Style::Default, sf::ContextSettings(24));
	SetIcon(window);
	window.setVerticalSyncEnabled(true);
	window.setActive(true);
	GLenum errorcode = glewInit();
	if (errorcode != GLEW_OK) {
		throw std::runtime_error(std::string(reinterpret_cast<const char*>(glewGetErrorString(errorcode))));
	}
	glEnable(GL_DEPTH_TEST);
	vector<string> meshes{ "models/cube.obj", "models/cube.obj" };
	vector<string> textures{ "textures/sila.jpg","textures/sila.jpg" };
	vector<string> vertes_s{ "shaders/vertex.vert","shaders/vertex.vert" };
	vector<string> frags_s{ "shaders/fragment.frag","shaders/fragment.frag" };
	Scene* s = new Scene(meshes,textures,vertes_s,frags_s);
	make_scene(s);
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
					s->move_airship(0.0f, 0.1f, 0);			break;
				case sf::Keyboard::A:
					s->move_airship(-0.1f, 0, 0);			break;
				case sf::Keyboard::S:
					s->move_airship(0.0f, -0.1f, 0);			break;
				case sf::Keyboard::D:
					s->move_airship(0.1f, 0, 0);				break;
				case sf::Keyboard::Q:
					s->move_airship(0.0f, 0, -0.1f);			break;
				case sf::Keyboard::E:
					s->move_airship(0.0f, 0, 0.1f);			break;

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

	// ������ ��������� ��������, ����� ��������� ������ �� ����� (image.loadFromFile("icon.png"))
	image.create(16, 16);
	for (int i = 0; i < 16; ++i)
		for (int j = 0; j < 16; ++j)
			image.setPixel(i, j, {
				(sf::Uint8)(i * 16), (sf::Uint8)(j * 16), 0 });

	wnd.setIcon(image.getSize().x, image.getSize().y, image.getPixelsPtr());
}
