#version 330 core
	out vec4 FragColor;
	
	struct Material {
	    sampler2D specular;
		float diffuse;
	    float shininess;
	}; 
	
	struct DirLight {
	    vec3 direction;
		
	    vec3 ambient;
	    vec3 diffuse;
	    vec3 specular;
	};
	
	struct PointLight {
	    vec3 position;
	    
	    float constant;
	    float linear;
	    float quadratic;
		
	    vec3 ambient;
	    vec3 diffuse;
	    vec3 specular;
	};
	
	struct SpotLight {
	    vec3 position;
	    vec3 direction;

	    float cutOff;
	    float outerCutOff;
	  
	    float constant;
	    float linear;
	    float quadratic;
	  
	    vec3 ambient;
	    vec3 diffuse;
	    vec3 specular;       
	};
	
	in vec3 FragPos;
	in vec3 Normal;
	in vec2 TexCoords;
	
	uniform vec3 viewPos;
	uniform DirLight dirLight;
	uniform PointLight pointLight;
	uniform SpotLight spotLight;
	uniform Material material;
	uniform sampler2D ourTexture;
	uniform int num;
	
	vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir);
	vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir);
	vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir);
	vec3 ApplyToonShading(vec3 color);
	vec3 CalculateMinnaert(PointLight light, vec3 normal, vec3 fragPos, float minnaertFactor);
	
	void main()
	{    
	    vec3 norm = normalize(Normal);
	    vec3 viewDir = normalize(viewPos - FragPos);
		vec3 result;

		if (num == 0 || num == 1) {
			result += CalcPointLight(pointLight, norm, FragPos, viewDir);    
		}
		else if (num == 2 || num == 3) {
			result += CalcPointLight(pointLight, norm, FragPos, viewDir);  
			result = ApplyToonShading(result);
		}
		else {
			result = CalculateMinnaert(pointLight, norm, FragPos, 0.5f);
		}
	    
	    // directional lighting
	    result = CalcDirLight(dirLight, norm, viewDir);

	    // point lights
	    result += CalcPointLight(pointLight, norm, FragPos, viewDir);    

	    // spot light
	    result += CalcSpotLight(spotLight, norm, FragPos, viewDir);    

	    
	    FragColor = vec4(result, 1.0);
	}
	
	// calculates the color when using a directional light.
	vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir)
	{
	    vec3 lightDir = normalize(-light.direction);

	    // diffuse shading
	    float diff = max(dot(normal, lightDir), 0.0);

	    // specular shading
	    vec3 reflectDir = reflect(-lightDir, normal);
	    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	    // combine results
	    vec3 ambient = light.ambient * vec3(texture(ourTexture, TexCoords));
	    vec3 diffuse = light.diffuse * diff * vec3(texture(ourTexture, TexCoords));
	    vec3 specular = light.specular * spec * vec3(texture(ourTexture, TexCoords));

	    return (ambient + diffuse + specular);
	}
	
	// calculates the color when using a point light.
	vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
	{
	    vec3 lightDir = normalize(light.position - fragPos);

	    // diffuse shading
	    float diff = max(dot(normal, lightDir), 0.0);

	    // specular shading
	    vec3 reflectDir = reflect(-lightDir, normal);
	    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	    // attenuation
	    float distance = length(light.position - fragPos);
	    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));    

	    // combine results
	    vec3 ambient = light.ambient * vec3(texture(ourTexture, TexCoords));
	    vec3 diffuse = light.diffuse * diff * vec3(texture(ourTexture, TexCoords));
	    vec3 specular = light.specular * spec * vec3(texture(ourTexture, TexCoords));

	    ambient *= attenuation;
	    diffuse *= attenuation;
	    specular *= attenuation;

	    return (ambient + diffuse + specular);
	}
	
	// calculates the color when using a spot light.
	vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
	{
	    vec3 lightDir = normalize(light.position - fragPos);

	    // diffuse shading
	    float diff = max(dot(normal, lightDir), 0.0);

	    // specular shading
	    vec3 reflectDir = reflect(-lightDir, normal);
	    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	    // attenuation
	    float distance = length(light.position - fragPos);
	    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));    

	    // spotlight intensity
	    float theta = dot(lightDir, normalize(-light.direction)); 
	    float epsilon = light.cutOff - light.outerCutOff;
	    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);

	    // combine results
	    vec3 ambient = light.ambient * vec3(texture(ourTexture, TexCoords));
	    vec3 diffuse = light.diffuse * diff * vec3(texture(ourTexture, TexCoords));
	    vec3 specular = light.specular * spec * vec3(texture(ourTexture, TexCoords));

	    ambient *= attenuation * intensity;
	    diffuse *= attenuation * intensity;
	    specular *= attenuation * intensity;

	    return (ambient + diffuse + specular);
	}

	vec3 ApplyToonShading(vec3 color)
	{
		
	    vec3 n2 = normalize(Normal);
		vec3 l2 = normalize(FragPos);
		float diff = 0.2+max(dot(n2,l2),0,0)
		if (diff < 0.4)
		 color = color∗0.3;
		else if (diff < 0.7)
		 clr = diffColor;
		else
		 clr = diffColor∗1.3; 
		return clr;
	}

	vec3 CalculateMinnaert(PointLight light, vec3 normal, vec3 fragPos, float minnaertFactor)
	{
		vec3 lightDir = normalize(light.position - fragPos);

	    float minnaertTerm = pow(max(dot(normal, normalize(lightDir)), 0.0), minnaertFactor);
	    
	    vec3 color = vec3(texture(ourTexture, TexCoords)) * minnaertTerm;
	
	    return color;
	}