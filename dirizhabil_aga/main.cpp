#include "Headers.h"
#include "Mesh.h"
#include "Shader.h"
#include "Scene.h"
#include <ctime>
#include <iostream>

using namespace std;


void SetIcon(sf::Window& wnd);

bool isCamActive = false;
bool isCamTouched = false;
glm::vec2 mousePos;
glm::vec2 mouseDelta;


static int random(int a, int b) {
	srand((unsigned int)time(NULL));
	return a + rand() % (b - a + 1);
}
float find_houses_dist(vector<Entity> houses, glm::vec3 new_house_c, Scene* s) {
	float min = MAXINT;
	for (size_t i = 0; i < houses.size(); i++)
	{
		auto dist = s->find_dist(houses[i].position, new_house_c);
		if (dist < min)
			min = dist;
	}
	return min;
}


static void make_scene(Scene* s) {
	vector < glm::vec2> poses;
	auto grass = Entity(&s->meshes[0], &s->shaders[0]);
	//grass.position += glm::vec3(0,0,-50.0f);
	grass.scaling(20, 20, 0);
	s->entities.push_back(grass);

	auto airship = Entity(&s->meshes[1], &s->shaders[1]);
	airship.moving(0.0f, 0.f, 30.0f);
	s->entities.push_back(airship);

	auto fir = Entity(&s->meshes[2], &s->shaders[2],"fir",5);
	fir.scaling(0.008, 0.008, 0.008);
	fir.rotating(90, glm::vec3{ 1.f,0.f,0.f });
	s->entities.push_back(fir);

	auto gift = Entity(&s->meshes[3], &s->shaders[3],"gift",2);
	gift.scaling(0.08, 0.08, 0.08);
	//gift.rotating(90, glm::vec3{ 1.f,0.f,0.f });
	s->entities.push_back(gift);


	vector<Entity> houses;
	int a=-17, b=17;
	for (size_t i = 6; i < s->meshes.size(); i++)
	{
		
		auto house = Entity(&s->meshes[i], &s->shaders[i], "house", 3);
		
		float x = random(a, b);
		float y = random(a, b);
		while (x * x + y * y < 6 || find_houses_dist(houses,glm::vec3(x,y,1),s)<7) {
			x = random(a, b);
			y = random(a, b);
		}
		house.scaling(0.005, 0.005, 0.005);
		house.set_pos(glm::vec3(x, y, 1));
		house.rotating(90, glm::vec3{ 1.f,0.f,0.f });
		s->entities.push_back(house);
		houses.push_back(house);
	}

	//for (size_t i = s->meshes.size() - 2; i < s->meshes.size(); i++)
	//{

	//	auto house = Entity(&s->meshes[i], &s->shaders[i], "", 3);

	//	float x = random(a, b);
	//	float y = random(a, b);
	//	while (x * x + y * y < 10 || find_houses_dist(houses, glm::vec3(x, y, 1), s) < 5) {
	//		x = random(a, b);
	//		y = random(a, b);
	//	}
	//	house.scaling(0.005, 0.005, 0.005);
	//	house.set_pos(glm::vec3(x, y, 1));
	//	house.rotating(90, glm::vec3{ 1.f,0.f,0.f });
	//	s->entities.push_back(house);
	//	houses.push_back(house);
	//}


	
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
	glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);
	vector<string> meshes{ "models/cube.obj", "models/car.obj", "models/fir.obj" , "models/gift.obj","models/car.obj","models/sign.obj", "models/house.obj"};
	vector<string> textures{ "textures/grass.jpg ","textures/car.png", "textures/fir.png", "textures/gift.png","textures/car.png","textures/sign.obj","textures/house.png" };
	vector<string> vertes_s{ "shaders/vertex.vert","shaders/vertex.vert", "shaders/fir_vertex.vert","shaders/vertex.vert","shaders/vertex.vert","shaders/vertex.vert","shaders/vertex.vert" };
	vector<string> frags_s{ "shaders/fragment.frag","shaders/fragment.frag","shaders/fragment.frag","shaders/gift_frag.frag","shaders/gift_frag.frag","shaders/gift_frag.frag","shaders/fragment.frag" };
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
					s->move_airship(0.0f, 0.1f, 0);	    	break;
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
				case sf::Keyboard::C:
					s->is_aiming = !s->is_aiming;
					break;
				case sf::Keyboard::Space:
					if (s->can_send) {
						s->set_gift();
						s->can_send = false;
					}
					break;
				default:
					break;
				}
			}
		}
		s->ResetClock();
		//glClearColor(0, 0, 0, 1);
	
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

