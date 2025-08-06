import { useSearchParams } from "react-router";

export function useFormQueryParams<T extends Record<string, string>>(
  allowedKeys: (keyof T)[],
): T {
  const [params] = useSearchParams();

  const entries = Array.from(params.entries()).filter(([key]) =>
    allowedKeys.includes(key as keyof T),
  ) as [keyof T, string][];

  return Object.fromEntries(entries) as T;
}
