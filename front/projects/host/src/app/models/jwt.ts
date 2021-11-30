export class Jwt
{
	accessToken: string;
	refreshToken: string;
	expireTime: Date;
}

export class LoginRequest
{
	username: string;
	password: string;
}

export class RegisterRequest
{
	username: string;
	email: string;
	password: string;
}
