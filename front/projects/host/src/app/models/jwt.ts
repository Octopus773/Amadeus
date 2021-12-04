export class Jwt
{
	access_token: string;
	refresh_token: string;
	expire_in: Date;
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
