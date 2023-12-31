#define _USE_MATH_DEFINES
#include <gl/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <cmath>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <iostream>



void SetIcon(sf::Window& wnd);
void Init();
void Draw();
void Release();
void InitVBO();
void InitShader();
void checkOpenGLerror();
void ReleaseVBO();
void ReleaseShader();
void ShaderLog(unsigned int shader);

struct Vertex {
    GLfloat x;
    GLfloat y;
    GLfloat z;
};

GLuint Program;
GLuint Attrib_vertex;
GLuint Attrib_color;
GLuint VBO;
GLuint VBO_color;
GLuint location;
float x = 1, y = 1, z = 1;

// �������� ��� ���������� �������
const char* VertexShaderSource = R"(
    #version 330 core
    in vec3 coord;
    in vec3 color;
    out vec3 outColor;

    uniform mat4 model;
    uniform mat4 view;
    uniform mat4 projection;

    void main() {      
        gl_Position = projection * view * model * vec4(coord, 1.0);
        outColor = color;
    }
)";


// �������� ��� ������������ �������
const char* FragShaderSource= R"(
    #version 330 core
    in vec3 outColor;
    out vec4 fragColor;
   
    void main() {
        fragColor = vec4(outColor, 1);
    }
)";

int main() {
    // ������ ����
    sf::Window window(sf::VideoMode(600, 600), "laba11", sf::Style::Default, sf::ContextSettings(32));
    // ������ ������ (���� � ��������� ��������� ��� ���������)
    SetIcon(window);
    // �������� ������������ ������������� (������������� ������� ��������� � �������� ������ ��������, ����� �������� �� �������, ������ ��� �� �����������)
    window.setVerticalSyncEnabled(true);

    // ���������� ����
    window.setActive(true);

    // �������������
    glewInit();
    Init();
    // ������� ���� ����
    while (window.isOpen()) {

        sf::Event event;
        // ���� ��������� ������� ����, ��� ����� ������ ������� ������, �������� ����� � ������ �����
        while (window.pollEvent(event)) {
            if (event.type == sf::Event::Closed) {
                // ���� �������, ������� �� �����
                window.close();
            }
            else if (event.type == sf::Event::Resized) {
                // ������� ������ ����, ���� �������� � ������ ������� Opengl ���������
                glViewport(0, 0, event.size.width, event.size.height);
            }
            else if (event.type == sf::Event::KeyPressed)
            {
                switch (event.key.code)
                {
                case sf::Keyboard::Key::D:
                    x += 0.1f;
                    break;
                case sf::Keyboard::Key::A:
                    x -= 0.1f;
                    break;
                case sf::Keyboard::Key::W:
                    y += 0.1f;
                    break;
                case sf::Keyboard::Key::S:
                    y -= 0.1f;
                    break;
                case sf::Keyboard::Key::Q:
                    z += 0.1f;
                    break;
                case sf::Keyboard::Key::E:
                    z -= 0.1f;
                    break;
                }
            }
        }
        // ������� �������
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        // ������ �����
        Draw();

        // ������������ ���� - ������ �������� � ������ ����� �������
        window.display();
    }
    Release();
    return 0;
}


// � ������ ������������� ������� ���������� �������� �������, ������� � ������ �����
void Release() {
    // �������
    ReleaseShader();
    // ��������� �����
    ReleaseVBO();

}
void ReleaseVBO() {
    glBindBuffer(GL_ARRAY_BUFFER, 0);
    glDeleteBuffers(1, &VBO);
}

void ReleaseShader() {
    // ��������� ����, �� ��������� ��������� ���������
    glUseProgram(0);
    // ������� ��������� ���������
    glDeleteProgram(Program);
}

void Init() {
    InitShader();
    InitVBO();
    glEnable(GL_DEPTH_TEST);
}

void InitShader() {
    // ������� ��������� ������
    GLuint vShader = glCreateShader(GL_VERTEX_SHADER);
    // �������� �������� ���
    glShaderSource(vShader, 1, &VertexShaderSource, NULL);
    // ����������� ������
    glCompileShader(vShader);
    std::cout << "vertex shader \n";
    // ������� ������ ���� �������
    ShaderLog(vShader);
    // ������� ����������� ������
    GLuint fShader = glCreateShader(GL_FRAGMENT_SHADER);
    // �������� �������� ���
    glShaderSource(fShader, 1, &FragShaderSource, NULL);
    // ����������� ������
    glCompileShader(fShader);
    std::cout << "fragment shader \n";
    // ������� ������ ���� �������
    ShaderLog(fShader);
    // ������� ��������� � ����������� ������� � ���
    Program = glCreateProgram();
    glAttachShader(Program, vShader);
    glAttachShader(Program, fShader);
    // ������� ��������� ���������
    glLinkProgram(Program);
    // ��������� ������ ������
    int link_ok;
    glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
    if (!link_ok) {
        std::cout << "error attach shaders \n";
        return;
    }
    // ���������� ID �������� �� ��������� ���������
    const char* attr_name = "coord"; //��� � �������

    Attrib_vertex = glGetAttribLocation(Program, attr_name);
    if (Attrib_vertex == -1) {
        std::cout << "could not bind attrib " << attr_name << std::endl;
        return;
    }

    const char* attr_name2 = "color"; //��� � �������
    Attrib_color = glGetAttribLocation(Program, attr_name2);
    if (Attrib_color == -1) {
        std::cout << "could not bind color " << attr_name2 << std::endl;
        return;
    }
    checkOpenGLerror();
}


void InitVBO() {
    glGenBuffers(1, &VBO); //glew.h
    float verts[] = {
        0.0f,1.0f,0.0f,    1.0f, 0.0f, 0.0f, 
        -1.0f,-1.0f,-1.0f,    0.0f, 1.0f, 0.0f,
        1.0f,-1.0f,-1.0f,    0.0f, 0.0f, 1.0f,

        0.0f,1.0f,0.0f,    1.0f, 1.0f, 0.0f,
        1.0f,-1.0f,-1.0f,    1.0f, 0.0f, 1.0f,
        0.0f,0.0f,1.0f,    1.0f, 0.0f, 0.0f,

        0.0f,1.0f,0.0f,    1.0f, 0.0f, 0.0f,
        0.0f,0.0f,1.0f,    1.0f, 1.0f, 0.0f,
        -1.0f,-1.0f,-1.0f,    0.0f, 1.0f, 0.0f,

        -1.0f,-1.0f,-1.0f,    1.0f, 0.0f, 0.0f,
        1.0f,-1.0f,-1.0f,    0.0f, 1.0f, 0.0f,
        0.0f,0.0f,1.0f,    1.0f, 0.0f, 1.0f,
    };



    glBindBuffer(GL_ARRAY_BUFFER, VBO);



    // �������� ������� � �����
    glBufferData(GL_ARRAY_BUFFER, sizeof(verts), verts, GL_STATIC_DRAW);
    checkOpenGLerror();
}


// ������� ��������������� ��������� �����
void Draw() {
    glUseProgram(Program); // ������������� ��������� ��������� �������



    glEnableVertexAttribArray(Attrib_vertex); // �������� ������ ���������
    glBindBuffer(GL_ARRAY_BUFFER, VBO); // ���������� VBO
    glVertexAttribPointer(Attrib_vertex, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)0);
    glEnableVertexAttribArray(Attrib_color);
    glVertexAttribPointer(Attrib_color, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)(3 * sizeof(float)));
    
    glm::mat4 model = glm::mat4(1.0f);

    model = glm::translate(model, glm::vec3(x, 0.0f, 0.0f));
    model = glm::translate(model, glm::vec3(0.0f, y, 0.0f));
    model = glm::translate(model, glm::vec3(0.0f, 0.0f, z));

    glm::mat4 view = glm::lookAt(glm::vec3(0.0f, 0.0f, 3.0f), glm::vec3(0.0f, 0.0f, 1.0f), glm::vec3(0.0f, 1.0f, 0.0f));
    glm::mat4 projection = glm::perspective(glm::radians(45.0f), 1.0f, 0.1f, 100.0f);

    glUniformMatrix4fv(glGetUniformLocation(Program, "model"), 1, GL_FALSE, &model[0][0]);
    glUniformMatrix4fv(glGetUniformLocation(Program, "view"), 1, GL_FALSE, &view[0][0]);
    glUniformMatrix4fv(glGetUniformLocation(Program, "projection"), 1, GL_FALSE, &projection[0][0]);

    
    glBindBuffer(GL_ARRAY_BUFFER, 0); // ��������� VBO
    glDrawArrays(GL_TRIANGLES, 0, 12); 
    glDisableVertexAttribArray(Attrib_vertex); // ��������� ������ ���������
    glDisableVertexAttribArray(Attrib_color); // ��������� ������ ���������
    glUseProgram(0); // ��������� ��������� ���������
    checkOpenGLerror();
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


void ShaderLog(unsigned int shader)
{
    int infologLen = 0;
    glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &infologLen);
    if (infologLen > 1)
    {
        int charsWritten = 0;
        std::vector<char> infoLog(infologLen);
        glGetShaderInfoLog(shader, infologLen, &charsWritten, infoLog.data());
        std::cout << "InfoLog: " << infoLog.data() << std::endl;
    }
}

void checkOpenGLerror() {
    GLenum err;
    if ((err = glGetError()) != GL_NO_ERROR)
    {
        std::cout << "OpenGl error!: " << err << std::endl;
    }
}