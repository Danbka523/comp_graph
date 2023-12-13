#define _USE_MATH_DEFINES
#include <gl/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <cmath>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <iostream>
#include <array>
#include <initializer_list>

#define deg2rad M_PI /180.0


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
};

GLuint Program;
GLuint Attrib_vertex;
GLuint Attrib_color;
GLuint VBO;
GLuint VBO_color;
GLuint location;
GLint Unif_xscale;
GLint Unif_yscale;
float x = 1.0f, y = 1.0f;

// �������� ��� ���������� �������
const char* VertexShaderSource = R"(
    #version 330 core
    in vec2 coord;
    in vec4 color;
    out vec4 outColor;


    uniform float scale_x;
    uniform float scale_y;
    
    void main() {      
       vec3 position = vec3(coord, 1.0) * mat3( scale_x,0,0,
                                                0,scale_y,0,
                                                0,0,1);
        gl_Position = vec4(position[0],position[1], 0.0, 1.0);
        outColor = color;
    }
)";


// �������� ��� ������������ �������
const char* FragShaderSource= R"(
    #version 330 core
    in vec4 outColor;
    out vec4 fragColor;
   
    void main() {
        fragColor = outColor;
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
                // ������ ������ ����, ���� �������� � ������ ������� Opengl ���������
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
   // glEnable(GL_DEPTH_TEST);
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

    // ���������� ID �������
    const char* unif_name = "scale_x";
    Unif_xscale = glGetUniformLocation(Program, unif_name);
    if (Unif_xscale == -1)
    {
        std::cout << "could not bind uniform " << unif_name << std::endl;
        return;
    }

    unif_name = "scale_y";
    Unif_yscale = glGetUniformLocation(Program, unif_name);
    if (Unif_yscale == -1)
    {
        std::cout << "could not bind uniform " << unif_name << std::endl;
        return;
    }


    checkOpenGLerror();
}

const int circleVertexCount = 360;

float bytify(float color)
{
    return (1 / 100.0) * color;
}

std::array<float, 4> HSVtoRGB(float hue, float saturation = 100.0, float value = 100.0)
{
    int sw = (int)floor(hue / 60) % 6;
    float vmin = ((100.0f - saturation) * value) / 100.0;
    float a = (value - vmin) * (((int)hue % 60) / 60.0);
    float vinc = vmin + a;
    float vdec = value - a;
    switch (sw)
    {
    case 0: return { bytify(value), bytify(vinc), bytify(vmin), 1.0 };
    case 1: return { bytify(vdec), bytify(value), bytify(vmin), 1.0 };
    case 2: return { bytify(vmin), bytify(value), bytify(vinc), 1.0 };
    case 3: return { bytify(vmin), bytify(vdec), bytify(value), 1.0 };
    case 4: return { bytify(vinc), bytify(vmin), bytify(value), 1.0 };
    case 5: return { bytify(value), bytify(vmin), bytify(vdec), 1.0 };
    }
    return { 0, 0, 0 , 0 };
}

void InitVBO() {
    glGenBuffers(1, &VBO); //glew.h
    glGenBuffers(1, &VBO_color);
    std::array<std::array<float, 4>, circleVertexCount * 3> colors = {};

    Vertex circle[circleVertexCount * 3] = {};
    for (int i = 0; i < circleVertexCount; i++) {
        circle[i * 3] = { 0.5f * (float)cos(i * (360.0 / circleVertexCount) * deg2rad), 0.5f * (float)sin(i * (360.0 / circleVertexCount) * deg2rad) };
        circle[i * 3 + 1] = { 0.5f * (float)cos((i + 1) * (360.0 / circleVertexCount) * deg2rad), 0.5f * (float)sin((i + 1) * (360.0 / circleVertexCount) * deg2rad) };
        circle[i * 3 + 2] = { 0.0f, 0.0f };
        colors[i * 3] = HSVtoRGB(i % 360);
        colors[i * 3 + 1] = HSVtoRGB((i + 1) % 360);
        colors[i * 3 + 2] = { 1.0, 1.0, 1.0, 1.0 };
    }

    // �������� ������� � �����
    glBindBuffer(GL_ARRAY_BUFFER, VBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(circle), circle, GL_STATIC_DRAW);
    glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
    glBufferData(GL_ARRAY_BUFFER, sizeof(colors), colors.data(), GL_STATIC_DRAW);
    checkOpenGLerror();
}


// ������� ��������������� ��������� �����
void Draw() {
    // ������������� ��������� ��������� �������
    glUseProgram(Program);

    glUniform1f(Unif_xscale, x);
    glUniform1f(Unif_yscale, y);


    // �������� ������� ���������
    glEnableVertexAttribArray(Attrib_vertex);
    glEnableVertexAttribArray(Attrib_color);

    // ���������� VBO_position
    glBindBuffer(GL_ARRAY_BUFFER, VBO);
    glVertexAttribPointer(Attrib_vertex, 2, GL_FLOAT, GL_FALSE, 0, 0);

    // ���������� VBO_color
    glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
    glVertexAttribPointer(Attrib_color, 4, GL_FLOAT, GL_FALSE, 0, 0);

    // ��������� VBO
    glBindBuffer(GL_ARRAY_BUFFER, 0);

    // �������� ������ �� ����������(������)
    glDrawArrays(GL_TRIANGLES, 0, circleVertexCount * 3);

    // ��������� ������� ���������
    glDisableVertexAttribArray(Attrib_vertex);
    glDisableVertexAttribArray(Attrib_color);

    glUseProgram(0);
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