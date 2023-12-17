#version 330 core

layout (location = 0) in vec3 pos;
layout (location = 1) in vec3 normal;
layout (location = 2) in vec2 texcoord;

out vec2 otexcoord;

uniform mat4 view;
uniform mat4 projection;
uniform mat4 models[5];
uniform float sizes[5];

void main(){
     mat4 model = models[gl_InstanceID];
     float size = sizes[gl_InstanceID];
     gl_Position = projection * view * model * vec4(pos * size, 1.0);
     otexcoord = vec2(texcoord.x, 1.0f - texcoord.y);
}