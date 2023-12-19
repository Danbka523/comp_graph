#version 330 core
	out vec4 FragColor;
	
	struct Material {
	    vec3 diffuse;
	    sampler2D specular;
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
	
	in vec3 opos;
	in vec3 onormal;
	in vec2 otexcoord;
	
	uniform vec3 viewPos;
	uniform DirLight dirLight;
	uniform PointLight pointLight;
	uniform SpotLight spotLight;
	uniform Material material;
	uniform sampler2D ourTexture;
	uniform int num;
	
	vec3 CalcDirLight(DirLight light, vec3 norm, vec3 viewDir);
	vec3 CalcPointLight(PointLight light, vec3 norm, vec3 opos, vec3 viewDir);
	vec3 CalcSpotLight(SpotLight light, vec3 norm, vec3 opos, vec3 viewDir);
	vec3 ApplyToonShading(vec3 color, vec3 norm, vec3 opos);
	//TODO ANY SHADING
	
	void main()
	{    
	    vec3 norm = normalize(onormal);
	    vec3 viewDir = normalize(viewPos - opos);
		vec3 result;


		//directional lighting
	    result = CalcDirLight(dirLight, norm, viewDir);

		//if (num==2)
	    	//result += ApplyToonShading(result);
	    

	    // point lights
	    result += CalcPointLight(pointLight, norm, opos, viewDir);    

	    //spotlight
	    result += CalcSpotLight(spotLight, norm, opos, viewDir);    

		//result = ApplyToonShading(result,norm,opos);    
	    FragColor = vec4(result, 1.0);
	}
	
	// calculates the color when using a directional light.
	vec3 CalcDirLight(DirLight light, vec3 norm, vec3 viewDir)
	{
		//ambient
		vec3 ambient = light.ambient * vec3(texture(ourTexture, otexcoord));

	    vec3 lightDir = normalize(-light.direction);
	    // diffuse shading
	    float diff = max(dot(norm, lightDir), 0.0);

	    // specular shading
	    vec3 reflectDir = reflect(-lightDir, norm);
	    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	    // combine results
	   
	    vec3 diffuse = light.diffuse * diff * vec3(texture(ourTexture, otexcoord));
	    vec3 specular = light.specular * spec * vec3(texture(ourTexture, otexcoord));

	    return (ambient + diffuse + specular);
	}
	
	// calculates the color when using a point light.
	vec3 CalcPointLight(PointLight light, vec3 onormal, vec3 opos, vec3 viewDir)
	{
	    vec3 lightDir = normalize(light.position - opos);

	    // diffuse shading
	    float diff = max(dot(onormal, lightDir), 0.0);

	    // specular shading
	    vec3 reflectDir = reflect(-lightDir, onormal);
	    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	    // attenuation
	    float distance = length(light.position - opos);
	    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));    

	    // combine results
	    vec3 ambient = light.ambient * vec3(texture(ourTexture, otexcoord));
	    vec3 diffuse = light.diffuse * diff * vec3(texture(ourTexture, otexcoord));
	    vec3 specular = light.specular * spec * vec3(texture(ourTexture, otexcoord));

	    ambient *= attenuation;
	    diffuse *= attenuation;
	    specular *= attenuation;

	    return (ambient + diffuse + specular);
	}
	
	// calculates the color when using a spot light.
	vec3 CalcSpotLight(SpotLight light, vec3 norm, vec3 opos, vec3 viewDir)
	{
	    vec3 lightDir = normalize(light.position - opos);
 		float theta = dot(lightDir, normalize(-light.direction)); 
	    if(theta > light.cutOff) // remember that we're working with angles as cosines instead of degrees so a '>' is used.
    	{    
			// ambient
			vec3 ambient = light.ambient * texture(ourTexture, otexcoord).rgb;
			
			// diffuse 
			float diff = max(dot(norm, lightDir), 0.0);
			vec3 diffuse = light.diffuse * diff * texture(ourTexture, otexcoord).rgb;  
			
			// specular
			vec3 reflectDir = reflect(-lightDir, norm);  
			float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
			vec3 specular = light.specular * spec * texture(material.specular, otexcoord).rgb;  
			
			// attenuation
			float distance    = length(light.position - opos);
			float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));    

			
			diffuse   *= attenuation;
			specular *= attenuation;   
				
			vec3 result = ambient + diffuse + specular;
			return result;
    	}
    	else 
    	{
			// else, use ambient light so scene isn't completely dark outside the spotlight.
			return vec3(light.ambient * texture(ourTexture, otexcoord).rgb);
       
    	}
	}

	vec3 ApplyToonShading(vec3 color, vec3 norm, vec3 opos)
	{
		vec3 lightDir = normalize(-dirLight.direction);
		float toon_intensity = max(dot(norm, vec3(0.0, 0.0, 1.0)), 0.0);


		
		if (toon_intensity > 0.95)
			color=color;
		else if (toon_intensity > 0.5)
			color= 0.8 * color;
		else if (toon_intensity > 0.25)
			color = 0.5* color;
		else
			color = 0.2* color;
		return color;
	}
