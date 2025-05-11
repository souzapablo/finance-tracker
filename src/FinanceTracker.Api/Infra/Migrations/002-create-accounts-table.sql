CREATE TABLE accounts (
    id BIGSERIAL,
    user_id BIGINT NOT NULL,
    name TEXT NOT NULL,
    balance DECIMAL(18,2) NOT NULL,
    created_at TIMESTAMPTZ DEFAULT NOW() NOT NULL,
    last_update TIMESTAMPTZ DEFAULT NOW() NOT NULL,
    is_deleted BOOLEAN DEFAULT FALSE NOT NULL,
    CONSTRAINT pk_accounts_id PRIMARY KEY (id),
    CONSTRAINT fk_accounts_id_user_id FOREIGN KEY (user_id) REFERENCES users (id)
);
